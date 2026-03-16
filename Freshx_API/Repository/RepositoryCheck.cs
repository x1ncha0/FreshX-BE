using Freshx_API.Models;
using Microsoft.EntityFrameworkCore;
using NuGet.Packaging.Signing;
using System.Linq.Expressions;
using System.Reflection;
using static Freshx_API.Repository.RepositoryCheck;
namespace Freshx_API.Repository
{
    public class DependencyCheckerResult
    {
        public bool CanDelete { get; set; }
        public bool CanSuspend { get; set; }
        public List<string> DependentTables { get; set; } = new();
        public List<string> ActiveDependencies { get; set; } = new();
        public string Message { get; set; }
    }
    public class RepositoryCheck
    {
        private readonly FreshxDBContext _context;

        public RepositoryCheck(FreshxDBContext context)
        {
            _context = context;
        }

        public async Task<bool> IsUniqueAsync<T>(string columnName, object value) where T : class
        {
            // Tạo parameter cho biểu thức LINQ
            var parameter = Expression.Parameter(typeof(T), "x");

            // Lấy thuộc tính tương ứng với columnName
            var property = Expression.Property(parameter, columnName);
            var constant = Expression.Constant(value);
            var equality = Expression.Equal(property, constant);
            var predicate = Expression.Lambda<Func<T, bool>>(equality, parameter);

            // Kiểm tra nếu bảng có trường IsDelete
            var isDeleteProperty = typeof(T).GetProperty("IsDelete");
            Expression<Func<T, bool>> isDeletedCheck = null;
            if (isDeleteProperty != null)
            {
                // Nếu có trường IsDelete, kiểm tra IsDelete != 1
                var isDeleteExpression = Expression.Property(parameter, isDeleteProperty);
                var deleteConstant = Expression.Constant(1);
                var notEqualExpression = Expression.NotEqual(isDeleteExpression, deleteConstant);
                isDeletedCheck = Expression.Lambda<Func<T, bool>>(notEqualExpression, parameter);

                // Kết hợp biểu thức kiểm tra tính duy nhất và kiểm tra trạng thái xóa
                var combinedPredicate = Expression.AndAlso(equality, isDeletedCheck.Body);
                var finalPredicate = Expression.Lambda<Func<T, bool>>(combinedPredicate, parameter);

                // Kiểm tra tính duy nhất với cả điều kiện IsDelete
                return await _context.Set<T>().AnyAsync(finalPredicate);
            }

            // Nếu không có trường IsDelete, chỉ kiểm tra tính duy nhất cho columnName
            return await _context.Set<T>().AnyAsync(predicate);
        }

        public async Task<bool> IsValueInUseAsync<T>(string columnName, object value) where T : class
        {
            var parameter = Expression.Parameter(typeof(T), "x");
            var property = Expression.Property(parameter, columnName);
            var constant = Expression.Constant(value);
            var equality = Expression.Equal(property, constant);
            var predicate = Expression.Lambda<Func<T, bool>>(equality, parameter);

            return await _context.Set<T>().AnyAsync(predicate);
        }

        public class EntityDependencyChecker
        {
            private readonly DbContext _context;

            public EntityDependencyChecker(DbContext context)
            {
                _context = context;
            }

            public async Task<DependencyCheckerResult> CheckDependencies<TEntity>(TEntity entity, string key) where TEntity : class
            {
                var result = new DependencyCheckerResult();
                var entityType = typeof(TEntity);
                var navigationProperties = GetNavigationProperties(entityType);

                foreach (var property in navigationProperties)
                {
                    var relatedType = property.PropertyType;
                    if (typeof(IEnumerable<>).IsAssignableFrom(relatedType))
                    {
                        relatedType = relatedType.GetGenericArguments()[0];
                    }

                    // Kiểm tra xem type có thuộc tính IsDeleted không
                    var hasIsDeleted = HasProperty(relatedType, "IsDeleted");
                    var hasIsSuspended = HasProperty(relatedType, "IsSuspended");

                    if (hasIsDeleted || hasIsSuspended)
                    {
                        var relatedEntities = await GetRelatedEntities(entity,property,key);

                        if (relatedEntities.Any())
                        {
                            result.DependentTables.Add(relatedType.Name);

                            if (hasIsDeleted)
                            {
                                var activeEntities = relatedEntities.Where(e =>
                                    (int)e.GetType().GetProperty("IsDeleted").GetValue(e) == 0).ToList();

                                if (activeEntities.Any())
                                {
                                    result.CanDelete = false;
                                    result.ActiveDependencies.Add($"{relatedType.Name} ({activeEntities.Count} active records)");
                                }
                            }

                            if (hasIsSuspended)
                            {
                                var activeEntities = relatedEntities.Where(e =>
                                    !(bool)e.GetType().GetProperty("IsSuspended").GetValue(e)).ToList();

                                if (activeEntities.Any())
                                {
                                    result.CanSuspend = false;
                                    result.ActiveDependencies.Add($"{relatedType.Name} ({activeEntities.Count} active records)");
                                }
                            }
                        }
                    }
                }

                if (!result.CanDelete && !result.CanSuspend)
                {
                    result.Message = $"Không thể sửa đổi trạng thái. Thực thể đang được sử dụng bởi: {string.Join(", ", result.ActiveDependencies)}";
                }
                else
                {
                    result.Message = "Không tìm thấy sự phụ thuộc đang hoạt động.";
                    result.CanDelete = true;
                    result.CanSuspend = true;
                }

                return result;
            }

            public async Task<bool> CascadeDelete<TEntity>(TEntity entity, string key) where TEntity : class
            {
                try
                {
                    var entityType = typeof(TEntity);
                    var navigationProperties = GetNavigationProperties(entityType);

                    foreach (var property in navigationProperties)
                    {
                        var relatedEntities = await GetRelatedEntities(entity, property, key);

                        foreach (var relatedEntity in relatedEntities)
                        {
                            var isDeletedProperty = relatedEntity.GetType().GetProperty("IsDeleted");
                            if (isDeletedProperty != null)
                            {
                                isDeletedProperty.SetValue(relatedEntity, 1);
                                _context.Entry(relatedEntity).State = EntityState.Modified;
                            }
                        }
                    }

                    var mainEntityIsDeletedProperty = entityType.GetProperty("IsDeleted");
                    if (mainEntityIsDeletedProperty != null)
                    {
                        mainEntityIsDeletedProperty.SetValue(entity, 1);
                        _context.Entry(entity).State = EntityState.Modified;
                    }

                    await _context.SaveChangesAsync();
                    return true;
                }
                catch (Exception ex)
                {
                    // Log exception
                    return false;
                }
            }

            public async Task<bool> CascadeSuspend<TEntity>(TEntity entity, string key) where TEntity : class
            {
                try
                {
                    var entityType = typeof(TEntity);
                    var navigationProperties = GetNavigationProperties(entityType);

                    foreach (var property in navigationProperties)
                    {
                        var relatedEntities = await GetRelatedEntities(entity, property, key);

                        foreach (var relatedEntity in relatedEntities)
                        {
                            var isSuspendedProperty = relatedEntity.GetType().GetProperty("IsSuspended");
                            if (isSuspendedProperty != null)
                            {
                                isSuspendedProperty.SetValue(relatedEntity, true);
                                _context.Entry(relatedEntity).State = EntityState.Modified;
                            }
                        }
                    }

                    var mainEntityIsSuspendedProperty = entityType.GetProperty("IsSuspended");
                    if (mainEntityIsSuspendedProperty != null)
                    {
                        mainEntityIsSuspendedProperty.SetValue(entity, true);
                        _context.Entry(entity).State = EntityState.Modified;
                    }

                    await _context.SaveChangesAsync();
                    return true;
                }
                catch (Exception ex)
                {
                    // Log exception
                    return false;
                }
            }

            private IEnumerable<PropertyInfo> GetNavigationProperties(Type type)
            {
                return type.GetProperties()
                    .Where(p =>
                        (p.PropertyType.IsGenericType &&
                         (typeof(IEnumerable<>).IsAssignableFrom(p.PropertyType.GetGenericTypeDefinition()) ||
                          typeof(ICollection<>).IsAssignableFrom(p.PropertyType.GetGenericTypeDefinition()))) ||
                        (!p.PropertyType.IsValueType && p.PropertyType != typeof(string)));
            }


            private bool HasProperty(Type type, string propertyName)
            {
                return type.GetProperty(propertyName) != null;
            }

            private async Task<IEnumerable<object>> GetRelatedEntities<TEntity>(TEntity entity, PropertyInfo navigationProperty, string? key) where TEntity : class
            {
                var relatedType = navigationProperty.PropertyType;

                // Kiểm tra keyProperty null
                var keyProperty = entity.GetType().GetProperty(key); // Assuming primary key is named "Id"
                if (keyProperty == null)
                {
                    throw new ArgumentException($"Property '{key}' not found on entity '{typeof(TEntity).Name}'");
                }
                var keyValue = keyProperty.GetValue(entity);

                // Kiểm tra relatedType có phải là kiểu generic không
                if (!relatedType.IsGenericType)
                {
                    throw new InvalidOperationException("The related type is not generic.");
                }

                // Tiến hành các bước tiếp theo nếu relatedType là kiểu generic
                var method = typeof(EntityFrameworkQueryableExtensions)
                    .GetMethod(nameof(EntityFrameworkQueryableExtensions.ToListAsync));

                var genericArgument = relatedType.GetGenericArguments()[0];
                var genericMethod = method.MakeGenericMethod(genericArgument);

                // Lấy dbSet từ _context
                var dbSet = _context.GetType().GetMethod("Set")
                    .MakeGenericMethod(genericArgument)
                    .Invoke(_context, null);

                // Xây dựng truy vấn với Entity Framework
                var query = ((IQueryable<object>)dbSet).Where(e => EF.Property<object>(e, navigationProperty.Name) == keyValue);

                return await (Task<IEnumerable<object>>)genericMethod.Invoke(null, new[] { query });
            }

        }
    

    }
    // Extension methods để dễ sử dụng
    public static class DependencyCheckerExtensions
    {
        public static async Task<DependencyCheckerResult> CheckDependencies<TEntity>(
            this DbContext context,
            TEntity entity, string key) where TEntity : class
        {
            var checker = new EntityDependencyChecker(context);
            return await checker.CheckDependencies(entity, key);
        }

        public static async Task<bool> CascadeDelete<TEntity>(
            this DbContext context,
            TEntity entity, string key) where TEntity : class
        {
            var checker = new EntityDependencyChecker(context);
            return await checker.CascadeDelete(entity, key);
        }

        public static async Task<bool> CascadeSuspend<TEntity>(
            this DbContext context,
            TEntity entity, string key) where TEntity : class
        {
            var checker = new EntityDependencyChecker(context);
            return await checker.CascadeSuspend(entity, key);
        }
    }
}

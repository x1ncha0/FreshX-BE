using Freshx_API.Models;
using Microsoft.EntityFrameworkCore;

namespace Freshx_API.Utilities
{
    public static class GenerateDepartmentCode
    {
        private static Dictionary<int, string>? _typePrefixes;
        private static readonly SemaphoreSlim _semaphore = new SemaphoreSlim(1, 1);
        private static async Task EnsurePrefixesLoaded(FreshxDBContext context)
        {
            if (_typePrefixes != null) return;

            try
            {
                await _semaphore.WaitAsync();
                if (_typePrefixes != null) return; // Double check after acquiring lock

                // Lấy tất cả department types từ database
                var departmentTypes = await context.DepartmentTypes
                    .Where(dt => dt.IsDeleted == 0)
                    .ToDictionaryAsync(
                        dt => dt.DepartmentTypeId,
                        dt => GetPrefix(dt.Code)
                    );

                _typePrefixes = departmentTypes;
            }
            finally
            {
                _semaphore.Release();
            }
        }

        private static string GetPrefix(string? typeCode)
        {
            return !string.IsNullOrEmpty(typeCode) ? typeCode : "XX";
        }

        public static async Task<(string code, string name)> GenerateCode(
            FreshxDBContext context,
            int departmentTypeId)
        {
            if (context == null)
                throw new ArgumentNullException(nameof(context));

            // Đảm bảo dictionary đã được load
            await EnsurePrefixesLoaded(context);

            if (_typePrefixes == null || !_typePrefixes.TryGetValue(departmentTypeId, out string? prefix))
                throw new ArgumentException($"Invalid department type ID: {departmentTypeId}");

            // Lấy department cuối cùng có prefix tương ứng
            var lastDepartment = await context.Departments
                .Where(d => d.Code.StartsWith(prefix) && d.IsDeleted == 0)
                .OrderByDescending(d => d.Code)
                .FirstOrDefaultAsync();

            int nextNumber;
            if (lastDepartment == null)
            {
                // Nếu chưa có department nào, bắt đầu từ 01
                nextNumber = 1;
            }
            else
            {
                // Lấy số thứ tự từ mã department cuối cùng
                string numberPart = lastDepartment.Code.Substring(prefix.Length);
                if (!int.TryParse(numberPart, out int lastNumber))
                {
                    // Trong trường hợp có lỗi format, bắt đầu lại từ 01
                    nextNumber = 1;
                }
                else
                {
                    nextNumber = lastNumber + 1;
                }
            }

            // Generate code và name
            string code = $"{prefix}{nextNumber:D2}";
            string name = await GenerateName(context, departmentTypeId, nextNumber);

            return (code, name);
        }

        private static async Task<string> GenerateName(FreshxDBContext context, int departmentTypeId, int number)
        {
            var departmentType = await context.DepartmentTypes
                .Where(dt => dt.DepartmentTypeId == departmentTypeId && dt.IsDeleted == 0)
                .FirstOrDefaultAsync();

            if (departmentType == null)
                throw new ArgumentException($"Invalid department type ID: {departmentTypeId}");

            return $"{departmentType.Name} {number:D2}";
        }

        // Optional: Method để reset cache nếu cần
        public static void ResetPrefixCache()
        {
            _typePrefixes = null;
        }
    }
}

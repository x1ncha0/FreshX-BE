using AutoMapper;
using FreshX.Application.Dtos.ServiceCatalog;
using FreshX.Application.Interfaces;
using FreshX.Application.Interfaces.ServiceType;
using FreshX.Domain.Entities;

namespace FreshX.Application.Services
{
    public class ServiceCatalogService(
        IServiceCatalogRepository repository,
        IServiceGroupRepository serviceGroupRepository,
        IServiceTypeRepository serviceTypeRepository,
        IMapper mapper) : IServiceCatalogService
    {
        public async Task<IReadOnlyList<ServiceCatalogDto>> GetAllAsync(
            string? searchKeyword,
            DateTime? createdDate,
            DateTime? updatedDate,
            int? status,
            CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();
            var entities = await repository.GetAllAsync(searchKeyword, createdDate, updatedDate, status);
            return mapper.Map<IReadOnlyList<ServiceCatalogDto>>(entities);
        }

        public async Task<ServiceCatalogDetailDto?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();
            var entity = await repository.GetByIdAsync(id);
            return entity is null ? null : mapper.Map<ServiceCatalogDetailDto>(entity);
        }

        public async Task<ServiceCatalogDto> CreateAsync(ServiceCatalogCreateUpdateDto dto, CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();
            await ValidateAsync(dto, null);

            var entity = mapper.Map<ServiceCatalog>(dto);
            var createdEntity = await repository.CreateAsync(entity);
            return mapper.Map<ServiceCatalogDto>(createdEntity);
        }

        public async Task<ServiceCatalogDto?> UpdateAsync(int id, ServiceCatalogCreateUpdateDto dto, CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();
            var existingEntity = await repository.GetByIdAsync(id);
            if (existingEntity is null)
            {
                return null;
            }

            await ValidateAsync(dto, existingEntity.Id);
            mapper.Map(dto, existingEntity);
            await repository.UpdateAsync(existingEntity);
            return mapper.Map<ServiceCatalogDto>(existingEntity);
        }

        public async Task<ServiceCatalogDto?> DeleteAsync(int id, CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();
            var existingEntity = await repository.GetByIdAsync(id);
            if (existingEntity is null)
            {
                return null;
            }

            await repository.DeleteAsync(id);
            return mapper.Map<ServiceCatalogDto>(existingEntity);
        }

        private async Task ValidateAsync(ServiceCatalogCreateUpdateDto dto, int? currentId)
        {
            var normalizedCode = NormalizeRequired(dto.Code, "Mã danh mục dịch vụ là bắt buộc.");
            dto.Code = normalizedCode;

            if (dto.Level is > 3)
            {
                throw new InvalidOperationException("Level phải nhỏ hơn hoặc bằng 3.");
            }

            var existingCatalog = await repository.GetByCodeAsync(normalizedCode);
            if (existingCatalog is not null && existingCatalog.Id != currentId)
            {
                throw new InvalidOperationException("Mã danh mục dịch vụ đã tồn tại.");
            }

            var serviceGroup = await serviceGroupRepository.GetByIdAsync(dto.ServiceGroupId ?? 0);
            if (serviceGroup is null)
            {
                throw new InvalidOperationException("Nhóm dịch vụ không hợp lệ.");
            }

            var serviceType = await serviceTypeRepository.GetByIdAsync(dto.ServiceTypeId ?? 0);
            if (serviceType is null)
            {
                throw new InvalidOperationException("Loại dịch vụ không hợp lệ.");
            }

            if (dto.ParentServiceId.HasValue)
            {
                if (currentId.HasValue && dto.ParentServiceId.Value == currentId.Value)
                {
                    throw new InvalidOperationException("Dịch vụ cha không hợp lệ.");
                }

                var parentService = await repository.GetByIdAsync(dto.ParentServiceId.Value);
                if (parentService is null)
                {
                    throw new InvalidOperationException("Dịch vụ cha không hợp lệ.");
                }

                var isParentServiceActive = await repository.CheckStatusByIdAsync(dto.ParentServiceId.Value);
                if (!isParentServiceActive)
                {
                    throw new InvalidOperationException("Dịch vụ cha đang tạm ngưng hoặc không khả dụng.");
                }
            }

            if (dto.IsParentService == true)
            {
                dto.ParentServiceId = null;
            }
        }

        private static string NormalizeRequired(string? value, string errorMessage)
        {
            var normalizedValue = value?.Trim();
            if (string.IsNullOrWhiteSpace(normalizedValue))
            {
                throw new InvalidOperationException(errorMessage);
            }

            return normalizedValue;
        }
    }
}

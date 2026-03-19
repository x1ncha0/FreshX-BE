using AutoMapper;
using FreshX.Application.Dtos.ServiceGroup;
using FreshX.Application.Interfaces;
using FreshX.Domain.Entities;

namespace FreshX.Application.Services
{
    public class ServiceGroupService(IServiceGroupRepository repository, IMapper mapper) : IServiceGroupService
    {
        public async Task<IReadOnlyList<ServiceGroupDto>> GetAllAsync(
            string? searchKeyword,
            DateTime? createdDate,
            DateTime? updatedDate,
            int? status,
            CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();
            var entities = await repository.GetAllAsync(searchKeyword, createdDate, updatedDate, status);
            return mapper.Map<IReadOnlyList<ServiceGroupDto>>(entities);
        }

        public async Task<IReadOnlyList<ServiceGroupDetailDto>> GetDetailAllAsync(
            string? searchKeyword,
            DateTime? createdDate,
            DateTime? updatedDate,
            int? status,
            CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();
            var entities = await repository.GetAllAsync(searchKeyword, createdDate, updatedDate, status);
            return mapper.Map<IReadOnlyList<ServiceGroupDetailDto>>(entities);
        }

        public async Task<ServiceGroupDetailDto?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();
            var entity = await repository.GetByIdAsync(id);
            return entity is null ? null : mapper.Map<ServiceGroupDetailDto>(entity);
        }

        public async Task<ServiceGroupDto> CreateAsync(ServiceGroupCreateUpdateDto dto, CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();
            var normalizedCode = NormalizeRequired(dto.Code, "Mã nhóm dịch vụ là bắt buộc.");
            var normalizedName = NormalizeRequired(dto.Name, "Tên nhóm dịch vụ là bắt buộc.");

            await EnsureUniqueCodeAsync(normalizedCode, null);
            await EnsureUniqueNameAsync(normalizedName, null);

            dto.Code = normalizedCode;
            dto.Name = normalizedName;

            var entity = mapper.Map<ServiceGroup>(dto);
            var createdEntity = await repository.CreateAsync(entity);
            return mapper.Map<ServiceGroupDto>(createdEntity);
        }

        public async Task<ServiceGroupDto?> UpdateAsync(int id, ServiceGroupCreateUpdateDto dto, CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();
            var existingEntity = await repository.GetByIdAsync(id);
            if (existingEntity is null)
            {
                return null;
            }

            var normalizedCode = NormalizeRequired(dto.Code, "Mã nhóm dịch vụ là bắt buộc.");
            var normalizedName = NormalizeRequired(dto.Name, "Tên nhóm dịch vụ là bắt buộc.");

            await EnsureUniqueCodeAsync(normalizedCode, existingEntity.Id);
            await EnsureUniqueNameAsync(normalizedName, existingEntity.Id);

            dto.Code = normalizedCode;
            dto.Name = normalizedName;

            mapper.Map(dto, existingEntity);
            await repository.UpdateAsync(existingEntity);
            return mapper.Map<ServiceGroupDto>(existingEntity);
        }

        public async Task<ServiceGroupDto?> DeleteAsync(int id, CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();
            var existingEntity = await repository.GetByIdAsync(id);
            if (existingEntity is null)
            {
                return null;
            }

            await repository.DeleteAsync(id);
            return mapper.Map<ServiceGroupDto>(existingEntity);
        }

        private async Task EnsureUniqueCodeAsync(string code, int? currentId)
        {
            var existingEntity = await repository.GetByCodeAsync(code);
            if (existingEntity is not null && existingEntity.Id != currentId)
            {
                throw new InvalidOperationException("Mã nhóm dịch vụ đã tồn tại.");
            }
        }

        private async Task EnsureUniqueNameAsync(string name, int? currentId)
        {
            var existingEntity = await repository.GetByNameAsync(name);
            if (existingEntity is not null && existingEntity.Id != currentId)
            {
                throw new InvalidOperationException("Tên nhóm dịch vụ đã tồn tại.");
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

using AutoMapper;
using FreshX.Application.Dtos.CommonDtos;
using FreshX.Application.Dtos.DepartmenTypeDtos;
using FreshX.Application.Interfaces;

namespace FreshX.Application.Services
{
    public class FixDepartmentTypeService(
        IFixDepartmentTypeRepository repository,
        IMapper mapper) : IFixDepartmentTypeService
    {
        public async Task<DepartmentTypeDto> CreateAsync(DepartmentTypeCreateUpdateDto request, CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();
            var entity = await repository.CreateDepartmentTypeAsync(request)
                ?? throw new InvalidOperationException("Tên loại phòng ban đã tồn tại.");

            return mapper.Map<DepartmentTypeDto>(entity);
        }

        public async Task<IReadOnlyList<DepartmentTypeDto>> GetAllAsync(Parameters parameters, CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();
            var entities = await repository.GetAllDepartmentTypeAsync(parameters);
            return mapper.Map<IReadOnlyList<DepartmentTypeDto>>(entities);
        }

        public async Task<DepartmentTypeDto?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();
            var entity = await repository.GetDepartmentTypeByIdAsync(id);
            return entity is null ? null : mapper.Map<DepartmentTypeDto>(entity);
        }

        public async Task<DepartmentTypeDto> UpdateAsync(int id, DepartmentTypeCreateUpdateDto request, CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();
            var entity = await repository.UpdateDepartmentTypeByIdAsync(id, request);
            if (entity is null)
            {
                var exists = await repository.GetDepartmentTypeByIdAsync(id);
                if (exists is null)
                {
                    throw new KeyNotFoundException("Loại phòng ban không tồn tại.");
                }

                throw new InvalidOperationException("Tên loại phòng ban đã tồn tại.");
            }

            return mapper.Map<DepartmentTypeDto>(entity);
        }

        public async Task<DepartmentTypeDto> DeleteAsync(int id, CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();
            var entity = await repository.DeleteDepartmentTypeByIdAsync(id)
                ?? throw new KeyNotFoundException("Loại phòng ban không tồn tại.");

            return mapper.Map<DepartmentTypeDto>(entity);
        }
    }
}

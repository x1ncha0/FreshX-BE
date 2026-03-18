using AutoMapper;
using FreshX.Application.Dtos.DepartmentDtos;
using FreshX.Application.Interfaces;
using FreshX.Domain.Entities;

namespace FreshX.Application.Services
{
    public class DepartmentService(IDepartmentRepository repository, IMapper mapper) : IDepartmentService
    {
        public async Task<IReadOnlyList<DepartmentDto>> GetAllAsync(
            string? searchKeyword,
            DateTime? createdDate,
            DateTime? updatedDate,
            int? status,
            CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();
            var departments = await repository.GetAllAsync(searchKeyword, createdDate, updatedDate, status);
            return mapper.Map<IReadOnlyList<DepartmentDto>>(departments);
        }

        public async Task<DepartmentDetailDto?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();
            var department = await repository.GetByIdAsync(id);
            return department is null ? null : mapper.Map<DepartmentDetailDto>(department);
        }

        public async Task<DepartmentDto> CreateAsync(DepartmentCreateUpdateDto dto, CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();
            await EnsureDepartmentTypeAsync(dto.DepartmentTypeId);

            var department = mapper.Map<Department>(dto);
            var createdDepartment = await repository.CreateAsync(department);
            return mapper.Map<DepartmentDto>(createdDepartment);
        }

        public async Task<DepartmentDto?> UpdateAsync(int id, DepartmentCreateUpdateDto dto, CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();
            var existingDepartment = await repository.GetByIdAsync(id);
            if (existingDepartment is null)
            {
                return null;
            }

            await EnsureDepartmentTypeAsync(dto.DepartmentTypeId);
            mapper.Map(dto, existingDepartment);
            await repository.UpdateAsync(existingDepartment);
            return mapper.Map<DepartmentDto>(existingDepartment);
        }

        public async Task<DepartmentDto?> DeleteAsync(int id, CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();
            var existingDepartment = await repository.GetByIdAsync(id);
            if (existingDepartment is null)
            {
                return null;
            }

            await repository.DeleteAsync(id);
            return mapper.Map<DepartmentDto>(existingDepartment);
        }

        private async Task EnsureDepartmentTypeAsync(int? departmentTypeId)
        {
            var departmentType = await repository.GetDepartmentTypeByIdAsync(departmentTypeId);
            if (departmentType is null)
            {
                throw new InvalidOperationException("Loại phòng ban không hợp lệ.");
            }
        }
    }
}

using FreshX.Application.Dtos.CommonDtos;
using FreshX.Application.Dtos.DepartmenTypeDtos;
using FreshX.Domain.Entities;

namespace FreshX.Application.Interfaces
{
    public interface IFixDepartmentTypeRepository
    {
        public Task<DepartmentType?> CreateDepartmentTypeAsync(DepartmentTypeCreateUpdateDto request);
        public Task<DepartmentType?> UpdateDepartmentTypeByIdAsync(int id, DepartmentTypeCreateUpdateDto request);
        public Task<List<DepartmentType>> GetAllDepartmentTypeAsync(Parameters parameters);
        public Task<DepartmentType?> GetDepartmentTypeByIdAsync(int id);
        public Task<DepartmentType?> DeleteDepartmentTypeByIdAsync(int id);
    }
}


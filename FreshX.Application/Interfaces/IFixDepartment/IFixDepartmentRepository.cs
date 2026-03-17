using FreshX.Application.Dtos.CommonDtos;
using FreshX.Application.Dtos.DepartmentDtos;
using FreshX.Domain.Entities;

namespace FreshX.Application.Interfaces
{
    public interface IFixDepartmentRepository
    {
        public Task<Department?> CreateDepartmentAsync(DepartmentCreateUpdateDto request);
        public Task<Department?> UpdateDepartmentAsync(int id, DepartmentCreateUpdateDto request);
        public Task<Department?> DeleteDepartmentAsync(int id);
        public Task<Department?> GetDepartmentByIdAsync(int id);
        public Task<List<Department>> GetAllDepartmentsAsync(Parameters parameters);
    }
}


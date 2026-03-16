using Freshx_API.Dtos.CommonDtos;
using Freshx_API.Dtos.DepartmentDtos;
using Freshx_API.Models;

namespace Freshx_API.Interfaces
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

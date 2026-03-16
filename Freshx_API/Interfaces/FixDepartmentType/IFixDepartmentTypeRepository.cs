using Freshx_API.Dtos.CommonDtos;
using Freshx_API.Dtos.DepartmenTypeDtos;
using Freshx_API.Models;

namespace Freshx_API.Interfaces
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

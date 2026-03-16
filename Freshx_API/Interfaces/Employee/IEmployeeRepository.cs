using Freshx_API.Dtos.CommonDtos;
using Freshx_API.Dtos.Employee;
using Freshx_API.Models;

namespace Freshx_API.Interfaces
{
    public interface IEmployeeRepository
    {
        public Task<Employee?> CreateEmployeeAsync(EmployeeRequest request);
        public Task<List<Employee?>> GetAllEmployeesAsync(Parameters parameters);
        public Task<Employee?> GetEmployeeByIdAsync(int id);
        public Task<Employee?> DeleteEmployeeByIdAsync(int id);
        public Task<Employee?> UpdateEmployeeByIdAsync(int id, EmployeeRequest request);
    }
}

using FreshX.Application.Dtos.CommonDtos;
using FreshX.Application.Dtos.Employee;
using FreshX.Domain.Entities;

namespace FreshX.Application.Interfaces
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


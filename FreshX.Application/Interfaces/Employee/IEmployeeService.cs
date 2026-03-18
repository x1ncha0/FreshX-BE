using FreshX.Application.Dtos.CommonDtos;
using FreshX.Application.Dtos.Employee;

namespace FreshX.Application.Interfaces
{
    public interface IEmployeeService
    {
        Task<EmployeeDto> CreateAsync(EmployeeRequest request, CancellationToken cancellationToken = default);
        Task<IReadOnlyList<EmployeeDto>> GetAsync(Parameters parameters, CancellationToken cancellationToken = default);
        Task<EmployeeDto?> GetByIdAsync(int id, CancellationToken cancellationToken = default);
        Task<EmployeeDto?> UpdateAsync(int id, EmployeeRequest request, CancellationToken cancellationToken = default);
        Task<EmployeeDto?> DeleteAsync(int id, CancellationToken cancellationToken = default);
    }
}

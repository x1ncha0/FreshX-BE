using AutoMapper;
using FreshX.Application.Dtos.CommonDtos;
using FreshX.Application.Dtos.Employee;
using FreshX.Application.Interfaces;

namespace FreshX.Application.Services
{
    public class EmployeeService(
        IEmployeeRepository employeeRepository,
        IDepartmentRepository departmentRepository,
        IPositionRepository positionRepository,
        IMapper mapper) : IEmployeeService
    {
        public async Task<EmployeeDto> CreateAsync(EmployeeRequest request, CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();
            await EnsureAssignmentAsync(request.PositionId, request.DepartmentId);

            var employee = await employeeRepository.CreateEmployeeAsync(request)
                ?? throw new InvalidOperationException("Không thể tạo nhân viên.");

            return mapper.Map<EmployeeDto>(employee);
        }

        public async Task<IReadOnlyList<EmployeeDto>> GetAsync(Parameters parameters, CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();
            var employees = await employeeRepository.GetAllEmployeesAsync(parameters);
            return mapper.Map<IReadOnlyList<EmployeeDto>>(employees);
        }

        public async Task<EmployeeDto?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();
            var employee = await employeeRepository.GetEmployeeByIdAsync(id);
            return employee is null ? null : mapper.Map<EmployeeDto>(employee);
        }

        public async Task<EmployeeDto?> UpdateAsync(int id, EmployeeRequest request, CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();
            await EnsureAssignmentAsync(request.PositionId, request.DepartmentId);

            var employee = await employeeRepository.UpdateEmployeeByIdAsync(id, request);
            return employee is null ? null : mapper.Map<EmployeeDto>(employee);
        }

        public async Task<EmployeeDto?> DeleteAsync(int id, CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();
            var employee = await employeeRepository.DeleteEmployeeByIdAsync(id);
            return employee is null ? null : mapper.Map<EmployeeDto>(employee);
        }

        private async Task EnsureAssignmentAsync(int? positionId, int? departmentId)
        {
            var position = await positionRepository.GetByIdAsync(positionId ?? 0)
                ?? throw new InvalidOperationException("Vai trò không hợp lệ.");

            var department = await departmentRepository.GetByIdAsync(departmentId ?? 0)
                ?? throw new InvalidOperationException("Phòng ban không hợp lệ.");

            var positionName = position.Name.Trim();
            var departmentName = department.Name?.Trim() ?? string.Empty;

            if (!positionName.StartsWith("Tiếp Nhận", StringComparison.OrdinalIgnoreCase)
                && !positionName.StartsWith("Thu Ngân", StringComparison.OrdinalIgnoreCase))
            {
                throw new InvalidOperationException("Phòng khám hoặc vai trò không hợp lệ.");
            }

            if (positionName.Equals("Tiếp Nhận", StringComparison.OrdinalIgnoreCase)
                && !departmentName.StartsWith("Phòng tiếp nhận", StringComparison.OrdinalIgnoreCase))
            {
                throw new InvalidOperationException("Nhân viên tiếp nhận chỉ có thể được phân vào phòng tiếp nhận.");
            }

            if (positionName.Equals("Thu Ngân", StringComparison.OrdinalIgnoreCase)
                && !departmentName.StartsWith("Phòng kế toán", StringComparison.OrdinalIgnoreCase))
            {
                throw new InvalidOperationException("Nhân viên thu ngân chỉ có thể được phân vào phòng kế toán.");
            }
        }
    }
}

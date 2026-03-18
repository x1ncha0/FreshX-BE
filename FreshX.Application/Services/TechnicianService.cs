using AutoMapper;
using FreshX.Application.Dtos;
using FreshX.Application.Dtos.CommonDtos;
using FreshX.Application.Dtos.Technician;
using FreshX.Application.Interfaces;

namespace FreshX.Application.Services
{
    public class TechnicianService(
        ITechnicianRepository technicianRepository,
        IDepartmentRepository departmentRepository,
        IPositionRepository positionRepository,
        IMapper mapper) : ITechnicianService
    {
        public async Task<TechnicianDto> CreateAsync(TechnicianRequest request, CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();
            await EnsureAssignmentAsync(request.PositionId, request.DepartmentId);

            var technician = await technicianRepository.CreateTechnicianAsync(request)
                ?? throw new InvalidOperationException("Không thể tạo kỹ thuật viên.");

            return mapper.Map<TechnicianDto>(technician);
        }

        public async Task<IReadOnlyList<TechnicianDto>> GetAsync(Parameters parameters, CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();
            var technicians = await technicianRepository.GetAllTechnicianAsync(parameters);
            return mapper.Map<IReadOnlyList<TechnicianDto>>(technicians);
        }

        public async Task<TechnicianDto?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();
            var technician = await technicianRepository.GetTechnicianByIdAsyn(id);
            return technician is null ? null : mapper.Map<TechnicianDto>(technician);
        }

        public async Task<TechnicianDto?> UpdateAsync(int id, TechnicianRequest request, CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();
            await EnsureAssignmentAsync(request.PositionId, request.DepartmentId);

            var technician = await technicianRepository.UpdateTechnicianByIdAsyn(id, request);
            return technician is null ? null : mapper.Map<TechnicianDto>(technician);
        }

        public async Task<TechnicianDto?> DeleteAsync(int id, CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();
            var technician = await technicianRepository.DeleteTechnicianByIdAsyn(id);
            return technician is null ? null : mapper.Map<TechnicianDto>(technician);
        }

        private async Task EnsureAssignmentAsync(int? positionId, int? departmentId)
        {
            var position = await positionRepository.GetByIdAsync(positionId ?? 0)
                ?? throw new InvalidOperationException("Vai trò không hợp lệ.");

            var department = await departmentRepository.GetByIdAsync(departmentId ?? 0)
                ?? throw new InvalidOperationException("Phòng ban không hợp lệ.");

            var positionName = position.Name.Trim();
            var departmentName = department.Name?.Trim() ?? string.Empty;

            if (!positionName.StartsWith("Kỹ Thuật", StringComparison.OrdinalIgnoreCase))
            {
                throw new InvalidOperationException("Vai trò không đúng hợp lệ.");
            }

            if (!departmentName.StartsWith("Phòng xét nghiệm", StringComparison.OrdinalIgnoreCase))
            {
                throw new InvalidOperationException("Phòng khám và vai trò không hợp lệ.");
            }
        }
    }
}

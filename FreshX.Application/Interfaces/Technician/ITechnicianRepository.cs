using FreshX.Application.Dtos.CommonDtos;
using FreshX.Application.Dtos.Technician;
using FreshX.Domain.Entities;
using System.Security.Permissions;

namespace FreshX.Application.Interfaces
{
    public interface ITechnicianRepository
    {
        public Task<Technician?> CreateTechnicianAsync(TechnicianRequest request);
        public Task<List<Technician?>> GetAllTechnicianAsync(Parameters parameters);
        public Task<Technician?> GetTechnicianByIdAsyn(int id);
        public Task<Technician?> DeleteTechnicianByIdAsyn(int id);
        public Task<Technician?> UpdateTechnicianByIdAsyn(int id, TechnicianRequest request);
    }
}


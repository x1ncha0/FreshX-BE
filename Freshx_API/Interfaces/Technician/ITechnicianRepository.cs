using Freshx_API.Dtos.CommonDtos;
using Freshx_API.Dtos.Technician;
using Freshx_API.Models;
using System.Security.Permissions;

namespace Freshx_API.Interfaces
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

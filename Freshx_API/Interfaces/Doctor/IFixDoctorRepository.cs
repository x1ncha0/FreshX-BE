using Freshx_API.Dtos;
using Freshx_API.Dtos.CommonDtos;
using Freshx_API.Models;
using System.Reflection.Metadata;

namespace Freshx_API.Interfaces
{
    public interface IFixDoctorRepository
    {
        public Task<Doctor?> CreateDoctorAsync(DoctorCreateUpdateDto request);
        public Task<List<Doctor?>> GetDoctorsAsync(Parameters parameters);
        public Task<Doctor?> GetDoctorByIdAsycn(int id);
        public Task<Doctor?> DeleteDoctorByIdAsync(int id);
        public Task<Doctor?> UpdateDoctorByIdAsync(int id,DoctorCreateUpdateDto request);
    }
}

using FreshX.Application.Dtos.CommonDtos;
using FreshX.Application.Dtos.Patient;
using FreshX.Domain.Entities;

namespace FreshX.Application.Interfaces
{
    public interface IPatientRepository
    {
        public Task<Patient?> CreatePatientAsync(AddingPatientRequest addingPatientRequest);
        public Task<Patient?> UpdatePatientByIdAsync(int id, UpdatingPatientRequest updatingPatientRequest);
        public Task<Patient?> GetPatientByIdAsync(int id);
        public Task<List<Patient?>> GetPatientsAsync(Parameters parameters);
        public Task<Patient?> DeletePatientByIdAsync(int id);
    }
}


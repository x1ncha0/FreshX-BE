using AutoMapper;
using Freshx_API.Dtos;
using Freshx_API.Interfaces;
using Freshx_API.Models;
using Microsoft.EntityFrameworkCore;

namespace Freshx_API.Repository
{

    public class MedicalServiceRequestRepository : IMedicalServiceRequestRepository
    {
        private readonly FreshxDBContext _context;

        public MedicalServiceRequestRepository(FreshxDBContext context)
        {
            _context = context;
        }

        public async Task<MedicalServiceRequest> GetByIdAsync(int id)
        {
            return await _context.MedicalServiceRequests
                            .Include(r => r.Service)
                            .ThenInclude(s => s.ServiceTypes)
                            .FirstOrDefaultAsync(msr => msr.MedicalServiceRequestId == id);
        }

        public async Task<IEnumerable<MedicalServiceRequest>> GetAllAsync()
        {
            return await _context.MedicalServiceRequests
                                 .Include(r => r.Service)
                                 .ThenInclude(s => s.ServiceTypes)
                                    .ToListAsync();
        }

        public async Task<MedicalServiceRequest> AddAsync(MedicalServiceRequest medicalServiceRequest)
        {
            _context.MedicalServiceRequests.Add(medicalServiceRequest);
            await _context.SaveChangesAsync();
            return medicalServiceRequest;
        }

        public async Task<MedicalServiceRequest> UpdateAsync(MedicalServiceRequest medicalServiceRequest)
        {
            _context.MedicalServiceRequests.Update(medicalServiceRequest);
            await _context.SaveChangesAsync();
            return medicalServiceRequest;
        }

        public async Task DeleteAsync(int id)
        {
            var entity = await GetByIdAsync(id);
            if (entity != null)
            {
                _context.MedicalServiceRequests.Remove(entity);
                await _context.SaveChangesAsync();
            }
        }
    }


}

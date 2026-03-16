using Freshx_API.Interfaces;
using Freshx_API.Models;
using Microsoft.EntityFrameworkCore;

namespace Freshx_API.Repository.Address
{
    public class AddressRepository : IAddressRepository
    {
        private readonly FreshxDBContext _context;

        public AddressRepository(FreshxDBContext context)
        {
            _context = context;
        }

        public async Task<List<Province>> GetAllProvincesAsync()
        {
            return await _context.Set<Province>().ToListAsync();
        }

        public async Task<Province> GetProvinceByCodeAsync(string code)
        {
            return await _context.Set<Province>().FirstOrDefaultAsync(p => p.Code == code);
        }

        public async Task<List<District>> GetDistrictsByProvinceCodeAsync(string provinceCode)
        {
            return await _context.Set<District>().Where(d => d.ProvinceCode == provinceCode).ToListAsync();
        }

        public async Task<District> GetDistrictByCodeAsync(string code)
        {
            return await _context.Set<District>().FirstOrDefaultAsync(d => d.Code == code);
        }

        public async Task<List<Ward>> GetWardsByDistrictCodeAsync(string districtCode)
        {
            return await _context.Set<Ward>().Where(w => w.DistrictCode == districtCode).ToListAsync();
        }

        public async Task<Ward> GetWardByCodeAsync(string code)
        {
            return await _context.Set<Ward>().FirstOrDefaultAsync(w => w.Code == code);
        }
    }

}

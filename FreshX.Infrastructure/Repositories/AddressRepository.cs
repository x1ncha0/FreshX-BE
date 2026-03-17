using FreshX.Application.Interfaces;
using FreshX.Domain.Entities;
using FreshX.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace FreshX.Infrastructure.Repositories;

public class AddressRepository(FreshXDbContext context) : IAddressRepository
{
    public Task<List<Province>> GetAllProvincesAsync() =>
        context.Provinces.AsNoTracking().OrderBy(x => x.Name).ToListAsync();

    public Task<Province> GetProvinceByCodeAsync(string code) =>
        context.Provinces.AsNoTracking().FirstAsync(x => x.Code == code);

    public Task<List<District>> GetDistrictsByProvinceCodeAsync(string provinceCode) =>
        context.Districts.AsNoTracking().Where(x => x.ProvinceCode == provinceCode).OrderBy(x => x.Name).ToListAsync();

    public Task<District> GetDistrictByCodeAsync(string code) =>
        context.Districts.AsNoTracking().FirstAsync(x => x.Code == code);

    public Task<List<Ward>> GetWardsByDistrictCodeAsync(string districtCode) =>
        context.Wards.AsNoTracking().Where(x => x.DistrictCode == districtCode).OrderBy(x => x.Name).ToListAsync();

    public Task<Ward> GetWardByCodeAsync(string code) =>
        context.Wards.AsNoTracking().FirstAsync(x => x.Code == code);
}
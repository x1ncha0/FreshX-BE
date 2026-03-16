using Freshx_API.Interfaces.ServiceType;
using Freshx_API.Models;

namespace Freshx_API.Services
{
    public class ServiceTypeService : IServiceTypeService
    {
        private readonly IServiceTypeRepository _repository;

        public ServiceTypeService(IServiceTypeRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<ServiceTypes>> GetAllAsync(string? searchKey)
        {
            return await _repository.GetAllAsync(searchKey);
        }

        public async Task<ServiceTypes?> GetByIdAsync(int id)
        {
            return await _repository.GetByIdAsync(id);
        }

        public async Task<ServiceTypes> AddAsync(ServiceTypes serviceType)
        {
            return await _repository.AddAsync(serviceType);
        }

        public async Task UpdateAsync(ServiceTypes serviceType)
        {
            await _repository.UpdateAsync(serviceType);
        }

        public async Task DeleteAsync(int id)
        {
            await _repository.DeleteAsync(id);
        }
    }

}

using FreshX.Application.Dtos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FreshX.Application.Interfaces.Services
{
    public interface ILabResultService
    {
        Task<IEnumerable<LabResultDto>> GetAllAsync(string searchKey = null);
        Task<LabResultDto?> GetByIdAsync(int id);
        Task AddAsync(CreateLabResultDto labResultDto);
        Task UpdateAsync(UpdateLabResultDto labResultDto);
        Task DeleteAsync(int id);
    }

}


using AutoMapper;
using Freshx_API.Dtos;
using Freshx_API.Models;
using Freshx_API.Interfaces.Auth;
using Freshx_API.Interfaces;
using Freshx_API.Interfaces.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Freshx_API.Services
{

    public class LabResultService : ILabResultService
    {
        private readonly ILabResultRepository _repository;
        private readonly IMapper _mapper;

        public LabResultService(ILabResultRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<LabResultDto>> GetAllAsync(string searchKey = null)
        {
            var labResults = await _repository.GetAllAsync(searchKey);
            return _mapper.Map<IEnumerable<LabResultDto>>(labResults);
        }

        public async Task<LabResultDto?> GetByIdAsync(int id)
        {
            var labResult = await _repository.GetByIdAsync(id);
            return _mapper.Map<LabResultDto>(labResult);
        }

        public async Task AddAsync(CreateLabResultDto labResultDto)
        {
            var labResult = _mapper.Map<LabResult>(labResultDto);
            labResult.CreatedDate = DateTime.Now;
            labResult.IsPaid = false;
            labResult.IsDeleted = 0;
            await _repository.AddAsync(labResult);
        }

        public async Task UpdateAsync(UpdateLabResultDto labResultDto)
        {
            var labResult = _mapper.Map<LabResult>(labResultDto);
            await _repository.UpdateAsync(labResult);
        }

        public async Task DeleteAsync(int id)
        {
            await _repository.DeleteAsync(id);
        }
    }
}

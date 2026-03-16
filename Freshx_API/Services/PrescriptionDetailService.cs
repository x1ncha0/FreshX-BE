using AutoMapper;
using Freshx_API.Dtos.Prescription;
using Freshx_API.Interfaces.IPrescription;
using Freshx_API.Models;

namespace Freshx_API.Services
{
    public class PrescriptionDetailService : IPrescriptionDetailService
    {
        private readonly IPrescriptionDetailRepository _repository;
        private readonly IMapper _mapper;

        public PrescriptionDetailService(IPrescriptionDetailRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task AddAsync(CreatePrescriptionDetailDto detailDto)
        {
            var detail = _mapper.Map<PrescriptionDetail>(detailDto);
            await _repository.AddAsync(detail);
        }

        public async Task UpdateAsync(UpdatePrescriptionDetailDto detailDto)
        {
            var detail = _mapper.Map<PrescriptionDetail>(detailDto);
            await _repository.UpdateAsync(detail);
        }

        public async Task DeleteAsync(int id)
        {
            var detail = await _repository.GetByIdAsync(id);
            if (detail != null)
            {
                await _repository.DeleteAsync(detail);
            }
        }

        public async Task<List<DetailDto>> GetByPrescriptionIdAsync(int prescriptionId)
        {
            var details = await _repository.GetByPrescriptionIdAsync(prescriptionId);

            return _mapper.Map<List<DetailDto>>(details);
        }
    }

}

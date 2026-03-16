using AutoMapper;
using Freshx_API.Dtos.Prescription;
using Freshx_API.Interfaces.IPrescription;
using Freshx_API.Models;
using Freshx_API.Repository;

namespace Freshx_API.Services.TmplPrescription
{
    public class TmplDetailService
    { 
        private readonly TmplDetailRepository _repository;
        private readonly IMapper _mapper;

        public TmplDetailService(TmplDetailRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task AddAsync(CreatePrescriptionDetailDto detailDto)
        {
            var detail = _mapper.Map<TemplatePrescriptionDetail>(detailDto);
            await _repository.AddAsync(detail);
        }

        public async Task UpdateAsync(UpdatePrescriptionDetailDto detailDto)
        {
            var detail = _mapper.Map<TemplatePrescriptionDetail>(detailDto);
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

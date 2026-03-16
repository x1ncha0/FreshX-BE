using AutoMapper;
using Freshx_API.Dtos.TmplPrescription;
using Freshx_API.Models;
using Freshx_API.Repository;
using Org.BouncyCastle.Crypto.Parameters;

namespace Freshx_API.Services.TmplPrescription
{
    public class TmplPrescriptionService 
    {
        private readonly TmplPrescriptionRepository _repository;
        private readonly IMapper _mapper;
        private readonly TmplDetailService _detailService;

        public TmplPrescriptionService(TmplPrescriptionRepository repository, IMapper mapper, TmplDetailService detailService)
        {
            _repository = repository;
            _mapper = mapper;
            _detailService = detailService;
        }

        public async Task<List<TmplPrescriptionDto>> GetAllAsync(string? searchKey)
        {
            var prescriptions = await _repository.GetAllAsync(searchKey);
            return _mapper.Map<List<TmplPrescriptionDto>>(prescriptions);
        }

        public async Task<TmplPrescriptionDto?> GetByIdAsync(int id)
        {
            var prescription = await _repository.GetByIdAsync(id);
            return _mapper.Map<TmplPrescriptionDto>(prescription);
        }

        public async Task<TmplPrescriptionDto> AddAsync(CreateTmplPrescriptionDto TmplPrescriptionDto)
        {
            var prescription = _mapper.Map<TemplatePrescription>(TmplPrescriptionDto);
            if (TmplPrescriptionDto.Details.Count != 0) {
                foreach (var detailDto in TmplPrescriptionDto.Details)
                {
                    
                    detailDto.PrescriptionId = prescription.TemplatePrescriptionId;
                    await _detailService.AddAsync(detailDto);
                }
            }
          var prescript = await _repository.AddAsync(prescription);
            return _mapper.Map<TmplPrescriptionDto>(prescript);
        }
           
        public async Task<TmplPrescriptionDto> UpdateAsync(UpdateTmplPrescriptionDto TmplPrescriptionDto)
        {
            var prescription = _mapper.Map<TemplatePrescription>(TmplPrescriptionDto);
            if (TmplPrescriptionDto.Details.Count != 0)
            {
                foreach (var detailDto in TmplPrescriptionDto.Details)
                {

                    detailDto.PrescriptionId = prescription.TemplatePrescriptionId;
                    await _detailService.UpdateAsync(detailDto);
                }
            }
          var prescript = await _repository.UpdateAsync(prescription);
            return _mapper.Map<TmplPrescriptionDto>(prescript);
        }

        public async Task DeleteAsync(int id)
        {
            await _repository.DeleteAsync(id);
        }
    }

}

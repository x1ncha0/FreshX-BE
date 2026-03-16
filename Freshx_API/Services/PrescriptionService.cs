using AutoMapper;
using Freshx_API.Dtos.Prescription;
using Freshx_API.Interfaces.IPrescription;
using Freshx_API.Models;

namespace Freshx_API.Services
{
    public class PrescriptionService : IPrescriptionService
    {
        private readonly IPrescriptionRepository _repository;
        private readonly IMapper _mapper;
        private readonly IPrescriptionDetailService _detailService;

        public PrescriptionService(IPrescriptionRepository repository, IMapper mapper, IPrescriptionDetailService detailService)
        {
            _repository = repository;
            _mapper = mapper;
            _detailService = detailService;
        }

        public async Task<List<PrescriptionDto>> GetAllAsync(string? searchKey)
        {
            var prescriptions = await _repository.GetAllAsync(searchKey);
            return _mapper.Map<List<PrescriptionDto>>(prescriptions);
        }

        public async Task<PrescriptionDto?> GetByIdAsync(int id)
        {
            var prescription = await _repository.GetByIdAsync(id);
            return _mapper.Map<PrescriptionDto>(prescription);
        }

        public async Task<PrescriptionDto> AddAsync(CreatePrescriptionDto prescriptionDto)
        {
            var prescription = _mapper.Map<Prescription>(prescriptionDto);
            if (prescriptionDto.Details.Count != 0) {
                foreach (var detailDto in prescriptionDto.Details)
                {
                    
                    detailDto.PrescriptionId = prescription.PrescriptionId;
                    await _detailService.AddAsync(detailDto);
                }
            }
          var prescript = await _repository.AddAsync(prescription);
            return _mapper.Map<PrescriptionDto>(prescript);
        }
           
        public async Task<PrescriptionDto> UpdateAsync(UpdatePrescriptionDto prescriptionDto)
        {
            var prescription = _mapper.Map<Prescription>(prescriptionDto);
            if (prescriptionDto.Details.Count != 0)
            {
                foreach (var detailDto in prescriptionDto.Details)
                {

                    detailDto.PrescriptionId = prescription.PrescriptionId;
                    await _detailService.UpdateAsync(detailDto);
                }
            }
          var prescript = await _repository.UpdateAsync(prescription);
            return _mapper.Map<PrescriptionDto>(prescript);
        }

        public async Task DeleteAsync(int id)
        {
            await _repository.DeleteAsync(id);
        }
    }

}

using AutoMapper;
using Freshx_API.Dtos.ExamineDtos;
using Freshx_API.Dtos.Prescription;
using Freshx_API.Interfaces;
using Freshx_API.Interfaces.IPrescription;
using Freshx_API.Models;

namespace Freshx_API.Services
{
  
    public class ExamineService : IExamineService
    {
        private readonly IExamineRepository _repository;
        private readonly IMapper _mapper;
        private readonly IPrescriptionService _prescriptionService;

        public ExamineService(IExamineRepository repository, IMapper mapper, IPrescriptionService prescriptionService)
        {
            _repository = repository;
            _mapper = mapper;
            _prescriptionService = prescriptionService;
        }

        public async Task<ExamineResponseDto> AddAsync(CreateExamDto dto)
        {
            var examine = _mapper.Map<Examine>(dto);

            examine.CreatedDate = DateTime.UtcNow;
            var createdExamine = await _repository.AddAsync(examine);
            return _mapper.Map<ExamineResponseDto>(createdExamine);
        }

        public async Task<ExamineResponseDto?> GetByIdAsync(int id)
        {
            var examine = await _repository.GetByIdAsync(id);
            return examine == null ? null : _mapper.Map<ExamineResponseDto>(examine);
        }

        public async Task<IEnumerable<ExamineResponseDto>> GetAllAsync()
        {
            var examines = await _repository.GetAllAsync();
            return _mapper.Map<IEnumerable<ExamineResponseDto>>(examines);
        }

        public async Task UpdateAsync(int id, ExamineRequestDto dto)
        {
            var examine = await _repository.GetByIdAsync(id);
            if (dto.Prescription != null)
            {
                if(dto.PrescriptionId != null)
                {
                    var updateprescription = _mapper.Map<UpdatePrescriptionDto>(dto.Prescription);
                    await _prescriptionService.UpdateAsync(updateprescription);
                }
                else
                {
                    var addprescription = await _prescriptionService.AddAsync(dto.Prescription);
                    examine.PrescriptionId = addprescription.PrescriptionId;
                }
            }
            if (examine != null)
            {
                _mapper.Map(dto, examine);
                await _repository.UpdateAsync(examine);
            }
        }

        public async Task DeleteAsync(int id)
        {
            await _repository.DeleteAsync(id);
        }
    }

}

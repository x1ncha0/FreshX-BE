using FreshX.Application.Dtos.ExamineDtos;

namespace FreshX.Application.Interfaces
{
    public interface IExamineService
    {
        Task<ExamineResponseDto> AddAsync(CreateExamDto dto);
        Task<ExamineResponseDto?> GetByIdAsync(int id);
        Task<IEnumerable<ExamineResponseDto>> GetAllAsync();
        Task UpdateAsync(int id, ExamineRequestDto dto);
        Task DeleteAsync(int id);
    }

}


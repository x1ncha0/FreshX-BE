using Freshx_API.Dtos.ExamineDtos;

namespace Freshx_API.Interfaces
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

using AutoMapper;
using Freshx_API.Dtos.DepartmentDtos;
using Freshx_API.Interfaces.Auth;
using Freshx_API.Interfaces;
using Freshx_API.Models;

namespace Freshx_API.Services
{
    public class DepartmentService
    {
        private readonly IDepartmentRepository _repository;
        private readonly IMapper _mapper;
        private readonly ITokenRepository _tokenRepository;

        public DepartmentService(IDepartmentRepository repository, IMapper mapper, ITokenRepository tokenRepository)
        {
            _repository = repository;
            _mapper = mapper;
            _tokenRepository = tokenRepository;
        }

        // Lấy danh sách phòng ban với các bộ lọc
        public async Task<IEnumerable<DepartmentDto>> GetAllAsync(string? searchKeyword,
            DateTime? createdDate, DateTime? updatedDate, int? status)
        {
            var entities = await _repository.GetAllAsync(searchKeyword, createdDate, updatedDate, status);

            // Kiểm tra trạng thái của DepartmentType trước khi trả về danh sách
            foreach (var department in entities.ToList())
            {
                if (department.DepartmentType != null)
                {
                    if (department.DepartmentType.IsDeleted != 0)
                    {
                        entities = entities.Where(d => d.DepartmentId != department.DepartmentId); // Loại bỏ phòng ban nếu DepartmentType bị xóa
                    }
                    else if (department.DepartmentType.IsSuspended != 0)
                    {
                        department.Name = department.Name + " (Tạm ngưng hoạt động)"; // Thêm thông báo tạm ngưng vào tên phòng ban
                    }
                }
            }
            // Sử dụng AutoMapper để chuyển đổi từ Model sang DTO
            return _mapper.Map<IEnumerable<DepartmentDto>>(entities);
        }

        // Lấy danh sách phòng ban chi tiết với các bộ lọc
        public async Task<IEnumerable<DepartmentDetailDto>> GetDetailAllAsync(string? searchKeyword,
            DateTime? createdDate, DateTime? updatedDate, int? status)
        {
            var entities = await _repository.GetAllAsync(searchKeyword, createdDate, updatedDate, status);


            // Sử dụng AutoMapper để chuyển đổi từ Model sang DTO
            return _mapper.Map<IEnumerable<DepartmentDetailDto>>(entities);
        }

        // Lấy phòng ban theo ID
        public async Task<DepartmentDto?> GetByIdAsync(int id)
        {
            var entity = await _repository.GetByIdAsync(id);

            if (entity == null) return null;

            // Kiểm tra trạng thái của DepartmentType trước khi trả về kết quả
            if (entity.DepartmentType != null)
            {
                if (entity.DepartmentType.IsDeleted != 0)
                {
                    return null; // Trả về null nếu DepartmentType bị xóa
                }
                else if (entity.DepartmentType.IsSuspended != 0)
                {
                    entity.Name = entity.Name + " (Tạm ngưng hoạt động)"; // Thêm thông báo tạm ngưng vào tên phòng ban
                }
            }

            return _mapper.Map<DepartmentDto>(entity);
        }

        // Tạo mới phòng ban
/*        public async Task<DepartmentDto> CreateAsync(DepartmentCreateUpdateDto dto)
        {


            var departmentType = await _repository.GetDepartmentTypeByIdAsync(dto.DepartmentTypeId); // Thêm một phương thức trong repository để lấy DepartmentType

            if (departmentType == null || departmentType.IsDeleted != 0)
            {
                throw new Exception("DepartmentType không hợp lệ hoặc đã bị xóa.");
            }

            if (departmentType.IsSuspended != 0)
            {
                dto.Name = dto.Name + " (Tạm ngưng hoạt động)"; // Thêm thông báo tạm ngưng vào tên phòng ban
            }

            var entity = _mapper.Map<Department>(dto);



            // Thiết lập giá trị mặc định
            entity.IsDeleted = 0;
            entity.CreatedDate = DateTime.UtcNow;
            entity.CreatedBy = _tokenRepository.GetUserIdFromToken();

            var createdEntity = await _repository.CreateAsync(entity);

            return _mapper.Map<DepartmentDto>(createdEntity);
        }

        // Cập nhật phòng ban*/
     /*   public async Task UpdateAsync(int id, DepartmentCreateUpdateDto dto)
        {
            var existingEntity = await _repository.GetByIdAsync(id);

            if (existingEntity == null)
                throw new KeyNotFoundException("Phòng ban không tồn tại.");

            // Kiểm tra trạng thái của DepartmentType trước khi cập nhật
            var departmentType = await _repository.GetDepartmentTypeByIdAsync(dto.DepartmentTypeId); // Thêm một phương thức trong repository để lấy DepartmentType

            if (departmentType == null || departmentType.IsDeleted != 0)
            {
                throw new Exception("DepartmentType không hợp lệ hoặc đã bị xóa.");
            }

            if (departmentType.IsSuspended != 0)
            {
                dto.Name = dto.Name + " (Tạm ngưng hoạt động)"; // Thêm thông báo tạm ngưng vào tên phòng ban
            }

            // Sử dụng AutoMapper để cập nhật dữ liệu từ DTO sang Entity
            _mapper.Map(dto, existingEntity);

            existingEntity.UpdatedDate = DateTime.UtcNow;
            existingEntity.UpdatedBy = _tokenRepository.GetUserIdFromToken();

            await _repository.UpdateAsync(existingEntity);
        }
*/
        // Xóa mềm phòng ban
        public async Task DeleteAsync(int id)
        {
            var entity = await _repository.GetByIdAsync(id);

            if (entity == null)
                throw new KeyNotFoundException("Phòng ban không tồn tại.");

            await _repository.DeleteAsync(id);
        }
    }
}

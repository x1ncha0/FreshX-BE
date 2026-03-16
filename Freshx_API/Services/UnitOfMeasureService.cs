using AutoMapper;
using Freshx_API.Dtos.UnitOfMeasure;
using Freshx_API.Interfaces.Auth;
using Freshx_API.Interfaces;
using Freshx_API.Models;
using Microsoft.EntityFrameworkCore;

namespace Freshx_API.Services
{
    public class UnitOfMeasureService
    {
        private readonly IUnitOfMeasureRepository _repository;
        private readonly IMapper _mapper;
        private readonly ITokenRepository _tokenRepository;

        public UnitOfMeasureService(IUnitOfMeasureRepository repository, IMapper mapper, ITokenRepository tokenRepository)
        {
            _repository = repository;
            _mapper = mapper;
            _tokenRepository = tokenRepository;
        }

        // Lấy danh sách đơn vị đo lường
        public async Task<IEnumerable<UnitOfMeasureDetailDto>> GetAllAsync(
            string? searchKeyword,
            DateTime? createdDate,
            DateTime? updatedDate,
            int? isSuspended,
            int? isDeleted)
        {
            var entities = await _repository.GetAllAsync(
                searchKeyword, createdDate, updatedDate, isSuspended, isDeleted);
            return _mapper.Map<IEnumerable<UnitOfMeasureDetailDto>>(entities);
        }

        // Lấy danh sách chi tiết đơn vị đo lường
        public async Task<IEnumerable<UnitOfMeasureDetailDto>> GetAllDetailAsync(
            string? searchKeyword,
            DateTime? createdDate,
            DateTime? updatedDate,
            int? isSuspended,
            int? isDeleted)
        {
            var entities = await _repository.GetAllAsync(
                searchKeyword, createdDate, updatedDate, isSuspended, isDeleted);
            return _mapper.Map<IEnumerable<UnitOfMeasureDetailDto>>(entities);
        }

        // Lấy thông tin đơn vị đo lường theo ID
        public async Task<UnitOfMeasureDetailDto?> GetByIdAsync(int id)
        {
            var entity = await _repository.GetByIdAsync(id);
            if (entity == null)
                return null;
            return _mapper.Map<UnitOfMeasureDetailDto>(entity);
        }

        public async Task<UnitOfMeasureDetailDto?> GetByCodeAsync(string code)
        {
            var entity = await _repository.GetByCodeAsync(code);
            if (entity == null)
                return null;
            return _mapper.Map<UnitOfMeasureDetailDto>(entity);
        }

        public async Task<UnitOfMeasureDetailDto?> GetNameAsync(string name)
        {
            var entity = await _repository.GetNameAsync(name);
            if (entity == null)
                return null;
            return _mapper.Map<UnitOfMeasureDetailDto>(entity);
        }


        // Tạo mới đơn vị đo lường
        public async Task<UnitOfMeasureDetailDto> CreateAsync(UnitOfMeasureCreateUpdateDto dto)
        {

            var existingEntity = await _repository.GetNameAsync(dto.Name);
            if (existingEntity != null)
            {
                // Nếu tên đã tồn tại, ném lỗi hoặc trả về thông báo lỗi
                throw new InvalidOperationException($"Đơn vị đo lường với tên '{dto.Name}' đã tồn tại.");
            }

            string code = GenerateUniqueCode();
            var entity = _mapper.Map<UnitOfMeasure>(dto);

            entity.Code = code;
            entity.IsDeleted = 0; // Đảm bảo giá trị mặc định cho trường IsDeleted
            entity.CreatedDate = DateTime.UtcNow; // Thêm ngày tạo
            entity.CreatedBy = _tokenRepository.GetUserIdFromToken(); // Lấy thông tin người tạo từ token

            var createdEntity = await _repository.CreateAsync(entity);

            // Trả về DTO của entity đã tạo
            return _mapper.Map<UnitOfMeasureDetailDto>(createdEntity);
        }

        // Cập nhật đơn vị đo lường theo ID
        public async Task UpdateAsyncId(int id, UnitOfMeasureCreateUpdateDto dto)
        {
            
            // Lấy entity hiện tại
            var existingEntity = await _repository.GetByIdAsync(id);
            if (existingEntity == null)
                throw new KeyNotFoundException("Đơn vị đo lường không tồn tại.");
            var existingEntityname = await _repository.GetNameAsync(dto.Name);
            if (existingEntityname != null)
            {
                // Nếu tên đã tồn tại, ném lỗi hoặc trả về thông báo lỗi
                throw new InvalidOperationException($"Đơn vị đo lường với tên '{dto.Name}' đã tồn tại.");
            }
            // Ánh xạ dữ liệu từ DTO vào entity hiện tại
            _mapper.Map(dto, existingEntity);

            // Cập nhật các trường thông tin
            existingEntity.UpdatedDate = DateTime.UtcNow;
            existingEntity.UpdatedBy = _tokenRepository.GetUserIdFromToken();

            // Lưu thay đổi vào cơ sở dữ liệu
            await _repository.UpdateAsync(existingEntity);
        }

        // Cập nhật đơn vị đo lường theo Code
        public async Task UpdateAsyncCode(string code, UnitOfMeasureCreateUpdateDto dto)
        {
            // Lấy entity hiện tại
            var existingEntity = await _repository.GetByCodeAsync(code);
            if (existingEntity == null)
                throw new KeyNotFoundException("Đơn vị đo lường không tồn tại.");
            var existingEntityname = await _repository.GetNameAsync(dto.Name);
            if (existingEntityname != null)
            {
                // Nếu tên đã tồn tại, ném lỗi hoặc trả về thông báo lỗi
                throw new InvalidOperationException($"Đơn vị đo lường với tên '{dto.Name}' đã tồn tại.");
            }
            // Ánh xạ dữ liệu từ DTO vào entity hiện tại
            _mapper.Map(dto, existingEntity);

            // Cập nhật các trường thông tin
            existingEntity.UpdatedDate = DateTime.UtcNow;
            existingEntity.UpdatedBy = _tokenRepository.GetUserIdFromToken();

            // Lưu thay đổi vào cơ sở dữ liệu
            await _repository.UpdateAsync(existingEntity);
        }

        // Xóa mềm đơn vị đo lường theo ID
        public async Task DeleteAsync(int id)
        {
            var entity = await _repository.GetByIdAsync(id);
            if (entity == null)
                throw new KeyNotFoundException("Đơn vị đo lường không tồn tại.");

            // Xóa mềm (cập nhật trạng thái IsDeleted)
            await _repository.DeleteAsync(id);
        }

        public async Task DeleteAsyncCode(string code)
        {
            var entity = await _repository.GetByCodeAsync(code);

            if (entity == null)
                throw new KeyNotFoundException("Nhà cung cấp không tồn tại.");

            await _repository.DeleteAsyncCode(code);
        }


        // Cập nhật trạng thái tạm ngưng của đơn vị đo lường
        public async Task SuspendAsync(int id, int isSuspended)
        {
            var existingEntity = await _repository.GetByIdAsync(id);
            if (existingEntity == null)
                throw new KeyNotFoundException("Đơn vị đo lường không tồn tại.");

            existingEntity.IsSuspended = isSuspended;
            existingEntity.UpdatedDate = DateTime.UtcNow;
            existingEntity.UpdatedBy = _tokenRepository.GetUserIdFromToken();

            await _repository.UpdateAsync(existingEntity);
        }

        private string GenerateUniqueCode()
        {
            return Guid.NewGuid().ToString("N").Substring(0, 8).ToUpper(); // Mã gồm 8 ký tự
        }

        
    }
}

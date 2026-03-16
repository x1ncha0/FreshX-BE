using AutoMapper;
using Freshx_API.Dtos.Supplier;
using Freshx_API.Interfaces.Auth;
using Freshx_API.Interfaces;
using Freshx_API.Models;

namespace Freshx_API.Services
{
    public class SupplierService
    {
        private readonly ISupplierRepository _repository;
        private readonly IMapper _mapper;
        private readonly ITokenRepository _tokenRepository;

        public SupplierService(ISupplierRepository repository, IMapper mapper, ITokenRepository tokenRepository)
        {
            _repository = repository;
            _mapper = mapper;
            _tokenRepository = tokenRepository;
        }

        // Lấy danh sách nhà cung cấp
        public async Task<IEnumerable<SupplierDetailDto>> GetAllAsync(
            string? searchKeyword,
            DateTime? createdDate,
            DateTime? updatedDate,
            bool? isSuspended,
            bool? isForeign,
            bool? isStateOwned,
            int? isDeleted)
        {
            var entities = await _repository.GetAllAsync(
                searchKeyword, createdDate, updatedDate, isSuspended, isForeign, isStateOwned, isDeleted);

            return _mapper.Map<IEnumerable<SupplierDetailDto>>(entities);
        }

        // Lấy danh sách chi tiết nhà cung cấp
        public async Task<IEnumerable<SupplierDetailDto>> GetAllDetailAsync(
            string? searchKeyword,
            DateTime? createdDate,
            DateTime? updatedDate,
            bool? isSuspended,
            bool? isForeign,
            bool? isStateOwned,
            int? isDeleted)
        {
            var entities = await _repository.GetAllAsync(
                searchKeyword, createdDate, updatedDate, isSuspended, isForeign, isStateOwned, isDeleted);

            return _mapper.Map<IEnumerable<SupplierDetailDto>>(entities);
        }

        // Lấy thông tin nhà cung cấp theo ID
        public async Task<SupplierDetailDto?> GetByIdAsync(int id)
        {
            var entity = await _repository.GetByIdAsync(id);

            if (entity == null)
                return null;

            return _mapper.Map<SupplierDetailDto>(entity);
        }

        // Tạo mới nhà cung cấp
        public async Task<SupplierDetailDto> CreateAsync(SupplierCreateDto dto)   
        {
            string code = GenerateUniqueCode();
            var entity = _mapper.Map<Supplier>(dto);

            entity.Code = code;
            entity.IsDeleted = 0;
            entity.CreatedDate = DateTime.UtcNow;
            entity.CreatedBy = _tokenRepository.GetUserIdFromToken();

            var createdEntity = await _repository.CreateAsync(entity);

            return _mapper.Map<SupplierDetailDto>(createdEntity);
        }

        // Cập nhật nhà cung cấp
        public async Task UpdateAsyncByCode(string code, SupplierUpdateDto dto)
        {
            var existingEntity = await _repository.GetSupplierByCodeAsync(code);

            if (existingEntity == null)
                throw new KeyNotFoundException("Nhà cung cấp không tồn tại.");

            _mapper.Map(dto, existingEntity);

            existingEntity.UpdatedDate = DateTime.UtcNow;
            existingEntity.UpdatedBy = _tokenRepository.GetUserIdFromToken();

            await _repository.UpdateAsync(existingEntity);
        }

        public async Task UpdateAsyncbyID(int id, SupplierUpdateDto dto)
        {
            var existingEntity = await _repository.GetByIdAsync(id);

            if (existingEntity == null)
                throw new KeyNotFoundException("Nhà cung cấp không tồn tại.");

            _mapper.Map(dto, existingEntity);

            existingEntity.UpdatedDate = DateTime.UtcNow;
            existingEntity.UpdatedBy = _tokenRepository.GetUserIdFromToken();

            await _repository.UpdateAsync(existingEntity);
        }

        // Xóa mềm nhà cung cấp
        public async Task DeleteAsyncId(int id)
        {
            var entity = await _repository.GetByIdAsync(id);

            if (entity == null)
                throw new KeyNotFoundException("Nhà cung cấp không tồn tại.");

            await _repository.DeleteAsyncId(id);
        }

        public async Task DeleteAsyncCode(string code)
        {
            var entity = await _repository.GetSupplierByCodeAsync(code);

            if (entity == null)
                throw new KeyNotFoundException("Nhà cung cấp không tồn tại.");

            await _repository.DeleteAsyncCode(code);
        }




        // Lấy thông tin nhà cung cấp theo code

        public async Task<SupplierDetailDto?> GetSupplierByCodeAsync(string code)
        {
            
            var entity = await _repository.GetSupplierByCodeAsync(code);

            if (entity == null)
                return null;

            return _mapper.Map<SupplierDetailDto>(entity);
        }

        // Hàm tạo mã duy nhất từ GUID
        private string GenerateUniqueCode()
        {
            return Guid.NewGuid().ToString("N").Substring(0, 8).ToUpper(); // Mã gồm 8 ký tự
        }
    }
}

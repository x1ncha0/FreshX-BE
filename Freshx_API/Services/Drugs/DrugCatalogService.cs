using AutoMapper;
using Freshx_API.Models;
using Freshx_API.Dtos;
using Freshx_API.Interfaces;
using Freshx_API.Dtos.DrugCatalog;
using Freshx_API.Interfaces.Auth;
using Freshx_API.Dtos.Country;
using Freshx_API.Dtos.Supplier;
using Freshx_API.Dtos.UnitOfMeasure;
using Freshx_API.Dtos.Drugs;

namespace Freshx_API.Services.Drugs
{
    public class DrugCatalogService
    {
        private readonly IDrugCatalogRepository _repository;
        private readonly IMapper _mapper;
        private readonly ITokenRepository _tokenRepository;

        public DrugCatalogService(IDrugCatalogRepository repository, IMapper mapper, ITokenRepository tokenRepository)
        {
            _repository = repository;
            _mapper = mapper;
            _tokenRepository = tokenRepository;
        }

        // Lấy danh sách danh mục thuốc với các bộ lọc
        public async Task<IEnumerable<DrugCatalogDetailDto>> GetAllAsync(string? searchKeyword,
            DateTime? createdDate, DateTime? updatedDate, int? status)
        {
            var entities = await _repository.GetAllAsync(searchKeyword, createdDate, updatedDate, status);
            // Sử dụng AutoMapper để chuyển đổi từ Model sang DTO
            return _mapper.Map<IEnumerable<DrugCatalogDetailDto>>(entities);
        }

        // Lấy danh mục thuốc theo ID
        public async Task<DrugCatalogDetailDto?> GetByIdAsync(int id)
        {
            var entity = await _repository.GetByIdAsync(id);
            if (entity == null) return null;
            return _mapper.Map<DrugCatalogDetailDto>(entity);
        }

        // Tạo mới danh mục thuốc
        public async Task<DrugCatalogDetailDto> CreateAsync(DrugCatalogCreateUpdateDto dto)
        {
            // Kiểm tra sự tồn tại của danh mục thuốc, nếu cần kiểm tra logic
            var entity = _mapper.Map<DrugCatalog>(dto);
            // Thiết lập giá trị mặc định
            entity.IsDeleted = 0;
            entity.CreatedDate = DateTime.UtcNow;
            entity.CreatedBy = _tokenRepository.GetUserIdFromToken();
            var createdEntity = await _repository.CreateAsync(entity);
            return _mapper.Map<DrugCatalogDetailDto>(createdEntity);
        }

        // Cập nhật danh mục thuốc
        public async Task UpdateAsync(int id, DrugCatalogCreateUpdateDto dto)
        {
            var existingEntity = await _repository.GetByIdAsync(id);
            if (existingEntity == null)
                throw new KeyNotFoundException("Danh mục thuốc không tồn tại.");
            // Sử dụng AutoMapper để cập nhật dữ liệu từ DTO sang Entity
            _mapper.Map(dto, existingEntity);
            existingEntity.UpdatedDate = DateTime.UtcNow;
            existingEntity.UpdatedBy = _tokenRepository.GetUserIdFromToken();
            await _repository.UpdateAsync(existingEntity);
        }

        // Xóa mềm danh mục thuốc
        public async Task DeleteAsync(int id)
        {
            var entity = await _repository.GetByIdAsync(id);
            if (entity == null)
                throw new KeyNotFoundException("Danh mục thuốc không tồn tại.");
            await _repository.DeleteAsync(id);
        }
        // Lấy nhà sản xuất theo ID
        public async Task<SupplierDetailDto?> GetManufacturerByIdAsync(int? manufacturerId)
        {
            var manufacturer = await _repository.GetManufacturerByIdAsync(manufacturerId);
            if (manufacturer == null) return null;
            return _mapper.Map<SupplierDetailDto>(manufacturer);
        }

        // Lấy đơn vị đo lường theo ID
        public async Task<UnitOfMeasureDetailDto?> GetUnitOfMeasureByIdAsync(int? unitOfMeasureId)
        {
            var unitOfMeasure = await _repository.GetUnitOfMeasureByIdAsync(unitOfMeasureId);
            if (unitOfMeasure == null) return null;
            return _mapper.Map<UnitOfMeasureDetailDto>(unitOfMeasure);
        }

        // Lấy quốc gia theo ID
        public async Task<CountryDto?> GetCountryByIdAsync(int? countryId)
        {
            var country = await _repository.GetCountryByIdAsync(countryId);
            if (country == null) return null;
            return _mapper.Map<CountryDto>(country);
        }

        // Lấy loại thuốc theo ID
        public async Task<DrugTypeDto?> GetDrugTypeByIdAsync(int? drugTypeId)
        {
            var drugType = await _repository.GetDrugTypeByIdAsync(drugTypeId);
            if (drugType == null) return null;
            return _mapper.Map<DrugTypeDto>(drugType);
        }
    }
}
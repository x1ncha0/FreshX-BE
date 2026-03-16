using AutoMapper;
using Freshx_API.Dtos.InventoryType;
using Freshx_API.Dtos.UnitOfMeasure;
using Freshx_API.Interfaces;
using Freshx_API.Models;

namespace Freshx_API.Services
{
    public class InventoryTypeService
    {
        private readonly IInventoryTypeRepository _repository;
        private readonly IMapper _mapper;

        public InventoryTypeService(IInventoryTypeRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        // Lấy danh sách tất cả loại tồn kho
        public async Task<IEnumerable<InventoryTypeDto>> GetAllAsync(string? searchKeyword = null)
        {
            var entities = await _repository.GetAllAsync(searchKeyword);
            return _mapper.Map<IEnumerable<InventoryTypeDto>>(entities);
        }

        // Lấy thông tin loại tồn kho theo ID
        public async Task<InventoryTypeDto?> GetByIdAsync(int id)
        {
            var entity = await _repository.GetByIdAsync(id);
            return entity == null ? null : _mapper.Map<InventoryTypeDto>(entity);
        }

       

        // Tạo mới loại tồn kho
        public async Task<InventoryTypeDto> CreateAsync(InventoryTypeCreateUpdateDto dto)
        {
            var existingEntity = await _repository.GetNameAsync(dto.Name);
            if (existingEntity != null)
            {
                // Nếu tên đã tồn tại, ném lỗi hoặc trả về thông báo lỗi
                throw new InvalidOperationException($"Inventory Type với tên '{dto.Name}' đã tồn tại.");
            }

            string code = GenerateUniqueCode();
            var entity = _mapper.Map<InventoryType>(dto);
            entity.Code = code;
            var createdEntity = await _repository.CreateAsync(entity);
            return _mapper.Map<InventoryTypeDto>(createdEntity);
        }


        // Cập nhật loại tồn kho theo ID 
        public async Task<bool> UpdateAsync(int id, InventoryTypeCreateUpdateDto dto)
        {
            var existingEntity = await _repository.GetByIdAsync(id);
            if (existingEntity == null) throw new KeyNotFoundException("Inventory Type không tồn tại.");
            var existingEntityname = await _repository.GetNameAsync(dto.Name);
            if (existingEntityname != null)
            {
                // Nếu tên đã tồn tại, ném lỗi hoặc trả về thông báo lỗi
                throw new InvalidOperationException($"Inventory Type với tên '{dto.Name}' đã tồn tại.");
            }
            _mapper.Map(dto, existingEntity);
            await _repository.UpdateAsync(existingEntity);
            return true;
        }

        // Cập nhật loại tồn kho theo Code
        public async Task<bool> UpdateAsyncCode(string code, InventoryTypeCreateUpdateDto dto)
        {
            var existingEntity = await _repository.GetByCodeAsync(code);
            if (existingEntity == null) throw new KeyNotFoundException("Inventory Type không tồn tại.");
            var existingEntityname = await _repository.GetNameAsync(dto.Name);
            if (existingEntityname != null)
            {
                // Nếu tên đã tồn tại, ném lỗi hoặc trả về thông báo lỗi
                throw new InvalidOperationException($"Inventory Type với tên '{dto.Name}' đã tồn tại.");
            }
            _mapper.Map(dto, existingEntity);
            await _repository.UpdateAsync(existingEntity);
            return true;
        }

        // Xóa loại tồn kho
        public async Task<bool> DeleteAsyncId(int id)
        {
            var entity = await _repository.GetByIdAsync(id);
            if (entity == null) return false;

            await _repository.DeleteAsync(id);
            return true;
        }
        public async Task<bool> DeleteAsyncCode(string code)
        {
            var entity = await _repository.GetByCodeAsync(code);
            if (entity == null) return false;

            await _repository.GetByCodeAsync(code);
            return true;
        }

        private string GenerateUniqueCode()
        {
            return Guid.NewGuid().ToString("N").Substring(0, 8).ToUpper(); // Mã gồm 8 ký tự
        }

        public async Task<InventoryTypeDto?> GetNameAsync(string name)
        {
            var entity = await _repository.GetNameAsync(name);
            if (entity == null)
                return null;
            return _mapper.Map<InventoryTypeDto>(entity);
        }

        public async Task<InventoryTypeDto?> GetByCodeAsync(string code)
        {
            var entity = await _repository.GetByCodeAsync(code);
            if (entity == null)
                return null;
            return _mapper.Map<InventoryTypeDto>(entity);
        }

    }
}

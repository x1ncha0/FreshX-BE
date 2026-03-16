namespace Freshx_API.Dtos.ServiceCatalog
{
    // DTO để trả dữ liệu cơ bản về danh mục dịch vụ (danh sách)
    public class ServiceCatalogDto
    {
        public int ServiceCatalogId { get; set; } // ID danh mục dịch vụ
        public string? Code { get; set; } // Mã danh mục dịch vụ
        public string? Name { get; set; } // Tên danh mục dịch vụ
        public decimal? Price { get; set; } // Giá dịch vụ
    }

   
}

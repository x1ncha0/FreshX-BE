using System;
using System.Collections.Generic;

namespace Freshx_API.Models;

public partial class Supplier
{
    public int SupplierId { get; set; } // ID nhà cung cấp
    public string? Code { get; set; } // Mã nhà cung cấp
    public string? Name { get; set; } // Tên nhà cung cấp
    public string? NameEnglish { get; set; } // Tên nhà cung cấp bằng tiếng Anh
    public string? NameVietNam { get; set; } // Tên nhà cung cấp bằng tiếng Nga
    public string? Address { get; set; } // Địa chỉ nhà cung cấp
    public int? WardId { get; set; } // ID phường/xã
    public int? DistrictId { get; set; } // ID quận/huyện
    public int? ProvinceId { get; set; } // ID tỉnh/thành phố
    public string? PhoneNumber { get; set; } // Số điện thoại nhà cung cấp
    public string? Fax { get; set; } // Số fax nhà cung cấp
    public string? Email { get; set; } // Địa chỉ email nhà cung cấp
    public string? TaxCode { get; set; } // Mã số thuế nhà cung cấp
    public string? Director { get; set; } // Giám đốc nhà cung cấp
    public string? ContactPerson { get; set; } // Người liên hệ của nhà cung cấp
    public bool? IsForeign { get; set; } // Trạng thái nhà cung cấp nước ngoài
    public bool IsStateOwned { get; set; } // Trạng thái nhà cung cấp nhà nước
    public bool IsSuspended { get; set; } // Trạng thái tạm ngưng
    public string? NameUnaccented { get; set; } // Tên không dấu của nhà cung cấp
    public DateTime? CreatedDate { get; set; } // Ngày tạo
    public string? CreatedBy { get; set; } // Người tạo
    public DateTime? UpdatedDate { get; set; } // Ngày cập nhật
    public string? UpdatedBy { get; set; } // Người cập nhật
    public bool IsPharmaceuticalSupplier { get; set; } // Trạng thái nhà cung cấp dược phẩm
    public bool IsMedicalConsumableSupplier { get; set; } // Trạng thái nhà cung cấp vật tư y tế
    public bool IsAssetSupplier { get; set; } // Trạng thái nhà cung cấp 
    public int? IsDeleted { get; set; } // Trạng thái đã xóa
    public virtual District? District { get; set; } // Đơn vị hành chính quận/huyện
    public virtual Province? Province { get; set; } // Đơn vị hành chính tỉnh/thành phố
    public virtual Ward? Ward { get; set; } // Đơn vị hành chính phường/xã
}

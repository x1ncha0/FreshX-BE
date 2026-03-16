using System;
using System.Collections.Generic;

namespace Freshx_API.Models;

public partial class Clinic
{
    public int ClinicId { get; set; } // ID bệnh viện

    public string? Code { get; set; } // Mã bệnh viện

    public string? Name { get; set; } // Tên bệnh viện

    public string? Address { get; set; } // Địa chỉ chi tiết
    public int? WardId { get; set; } // ID phường/xã

    public int? DistrictId { get; set; } // ID quận/huyện

    public int? ProvinceId { get; set; } // ID tỉnh/thành phố
    public string? PhoneNumber1 { get; set; } // Số điện thoại 1

    public string? PhoneNumber2 { get; set; } // Số điện thoại 2

    public string? PhoneNumber3 { get; set; } // Số điện thoại 3

    public int? IsSuspended { get; set; } // Trạng thái tạm ngưng

    public int? CreatedBy { get; set; } // Người tạo

    public DateTime? CreatedDate { get; set; } // Ngày tạo

    public int? UpdatedBy { get; set; } // Người cập nhật

    public DateTime? UpdatedDate { get; set; } // Ngày cập nhật

    public int? IsDeleted { get; set; } // Trạng thái đã xóa
    public virtual District? District { get; set; } // Đơn vị hành chính quận/huyện
    public virtual Province? Province { get; set; } // Đơn vị hành chính tỉnh/thành phố
    public virtual Ward? Ward { get; set; } // Đơn vị hành chính phường/xã
}

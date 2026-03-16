using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Freshx_API.Models;

public partial class Doctor
{
    [Key]
    public int DoctorId { get; set; } // ID bác sĩ
    public string? IdentityCardNumber { get; set; } // Số CMND/CCCD ktra trùng lặp
    public string? Name { get; set; } // Tên bác sĩ
    public string? Specialty { get; set; } // chuyên ngành và chuyên môn
    public string? Phone { get; set; } // Số điện thoại
    public string? Email { get; set; } // Email 
    public string? Gender { get; set; } //giới tính
    public DateTime? DateOfBirth { get; set; } // Ngày sinh
    public int? AvataId { get; set; } // ảnh
    public int? IsSuspended { get; set; } // Trạng thái tạm ngưng
    public string? CreatedBy { get; set; } // Người tạo

    public DateTime? CreatedDate { get; set; } // Ngày tạo

    public string? UpdatedBy { get; set; } // Người cập nhật

    public DateTime? UpdatedDate { get; set; } // Ngày cập nhật

    public int? IsDeleted { get; set; } // Trạng thái đã xóa
    public string? WardId { get; set; } // ID phường/xã

    public string? DistrictId { get; set; } // ID quận/huyện

    public string? ProvinceId { get; set; } // ID tỉnh/thành phố

    public int? DepartmentId { get; set; } // ID phòng ban

    public string? AccountId {  get; set; }
    public string? Address { get; set; }
    // Địa chỉ chi tiết bệnh nhân
    // Computed property for formatting
    [NotMapped]
    public string? FormattedAddress => string.Join(", ", new[]
        {
       Ward?.FullName,
        District?.FullName,
        Province?.FullName
    }.Where(x => !string.IsNullOrWhiteSpace(x)));
    public int? PositionId { get; set; }
    public virtual Department? Department { get; set; } // Phòng ban của nhân viên

    //  public virtual ICollection<DiagnosticImagingResult> DiagnosticImagingResults { get; set; } = new List<DiagnosticImagingResult>(); // Danh sách kết quả chẩn đoán hình ảnh

    public virtual District? District { get; set; } // Đơn vị hành chính quận/huyện

    public virtual Province? Province { get; set; } // Đơn vị hành chính tỉnh/thành phố

    public virtual Ward? Ward { get; set; } // Đơn vị hành chính phường/xã
    public virtual AppUser? AppUser { get; set; }
    public virtual ICollection<Reception> Receptions { get; set; } = new List<Reception>(); // Danh sách tiếp nhận
    public virtual Savefile? Avata { get; set; }
    public virtual Position? Position { get; set; }
}

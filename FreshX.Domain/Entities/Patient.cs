using FreshX.Domain.Common;

namespace FreshX.Domain.Entities;

public partial class Patient : BaseEntity
{
    public string? MedicalRecordNumber { get; set; } // Số hồ sơ bệnh án {tự sinh} vd: bn001}

    public string? IdentityCardNumber { get; set; } // Số CMND/CCCD ktra trùng lặp

    public string? AdmissionNumber { get; set; } // Số nhập viện {tự sinh vd: nv171224001 kết hợp giữa ngày nhập viện và số thứ tuwh trong ngày}

    public string? Name { get; set; } // Tên bệnh nhân

    public string? Gender { get; set; } // Giới tính bệnh nhân

    public DateTime? DateOfBirth { get; set; } // Ngày sinh bệnh nhân

    public string? PhoneNumber { get; set; } // Số điện thoại bệnh nhân

    /// <summary>
    /// Địa chỉ chi tiết bệnh nhân
    /// </summary>
    public string? Address { get; set; }

    public string? Email { get; set; }

    // Computed property for formatting
    public string? FormattedAddress => string.Join(", ", new[]
        {
        Ward?.FullName,
        District?.FullName,
        Province?.FullName
    }.Where(x => !string.IsNullOrWhiteSpace(x)));

    public string? WardId { get; set; } // ID phường/xã

    public string? DistrictId { get; set; } // ID quận/huyện

    public string? ProvinceId { get; set; } // ID tỉnh/thành phố

    public int? ImageId { get; set; } // Id đến ảnh bệnh nhân

    public string? Ethnicity { get; set; } // Dân tộc của bệnh nhân

    public string? AccountId { get; set; }

    public virtual District? District { get; set; } // Đơn vị hành chính quận/huyện

    public virtual Province? Province { get; set; } // Đơn vị hành chính tỉnh/thành phố

    public virtual Ward? Ward { get; set; } // Đơn vị hành chính phường/xã

    public virtual Savefile? Image { get; set; }

    public virtual AppUser? AppUser { get; set; }
}
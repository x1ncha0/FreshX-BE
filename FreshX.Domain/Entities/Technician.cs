using FreshX.Domain.Common;

namespace FreshX.Domain.Entities;

public partial class Technician : BaseEntity
{
    /// <summary>
    /// Tên kỹ thuật viên
    /// </summary>
    public string? Name { get; set; }

    /// <summary>
    /// Số CMND/CCCD ktra trùng lặp
    /// </summary>
    public string? IdentityCardNumber { get; set; }

    public string? Gender { get; set; }

    public string? PhoneNumber { get; set; }

    /// <summary>
    /// ảnh
    /// </summary>
    public int? AvataId { get; set; } 

    public string? Email { get; set; }

    /// <summary>
    /// Ngày sinh nhân viên
    /// </summary>
    public DateTime? DateOfBirth { get; set; }

    /// <summary>
    /// ID phường/xã
    /// </summary>
    public string? WardId { get; set; }

    /// <summary>
    /// ID quận/huyện
    /// </summary>
    public string? DistrictId { get; set; }

    /// <summary>
    /// ID tỉnh/thành phố
    /// </summary>
    public string? ProvinceId { get; set; }

    /// <summary>
    /// ID phòng ban
    /// </summary>
    public int? DepartmentId { get; set; }

    public string? AccountId { get; set; }


    /// <summary>
    /// Địa chỉ chi tiết bệnh nhân
    /// </summary>
    public string? Address { get; set; }


    /// <summary>
    /// Computed property for formatting
    /// </summary>
    public string? FormattedAddress => string.Join(", ", new[]
        {
       Ward?.FullName,
        District?.FullName,
        Province?.FullName
    }.Where(x => !string.IsNullOrWhiteSpace(x)));

    public int? PositionId { get; set; }

    /// <summary>
    /// Phòng ban của nhân viên
    /// </summary>
    public virtual Department? Department { get; set; }

    /// <summary>
    /// Đơn vị hành chính quận/huyện
    /// </summary>
    public virtual District? District { get; set; }

    /// <summary>
    /// Đơn vị hành chính tỉnh/thành phố
    /// </summary>
    public virtual Province? Province { get; set; }

    /// <summary>
    /// Đơn vị hành chính phường/xã
    /// </summary>
    public virtual Ward? Ward { get; set; }

    public virtual AppUser? AppUser { get; set; }

    public virtual Position? Position { get; set; }

    public virtual Savefile? Avata { get; set; }
}
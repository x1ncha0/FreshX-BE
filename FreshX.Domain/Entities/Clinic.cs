using FreshX.Domain.Common;

namespace FreshX.Domain.Entities;

public partial class Clinic : BaseEntity
{
    /// <summary>
    /// Mã bệnh viện
    /// </summary>
    public string? Code { get; set; }

    /// <summary>
    /// Tên bệnh viện
    /// </summary>
    public string? Name { get; set; }

    /// <summary>
    /// Địa chỉ chi tiết
    /// </summary>
    public string? Address { get; set; }

    /// <summary>
    /// ID phường/xã
    /// </summary>
    public int? WardId { get; set; }

    /// <summary>
    /// ID quận/huyện
    /// </summary>
    public int? DistrictId { get; set; }

    /// <summary>
    /// ID tỉnh/thành phố
    /// </summary>
    public int? ProvinceId { get; set; }

    /// <summary>
    /// Số điện thoại 1
    /// </summary>
    public string? PhoneNumber1 { get; set; }

    /// <summary>
    /// Số điện thoại 2
    /// </summary>
    public string? PhoneNumber2 { get; set; }

    /// <summary>
    /// Số điện thoại 3
    /// </summary>
    public string? PhoneNumber3 { get; set; }

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
}
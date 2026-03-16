using FreshX.Domain.Common;

namespace FreshX.Domain.Entities;

public partial class Country : BaseEntity
{
    /// <summary>
    /// Mã quốc gia
    /// </summary>
    public string? Code { get; set; }

    /// <summary>
    /// Tên quốc gia
    /// </summary>
    public string? Name { get; set; }

    /// <summary>
    /// Tên quốc gia bằng tiếng Anh
    /// </summary>
    public string? NameEnglish { get; set; }

    /// <summary>
    /// Tên quốc gia bằng tiếng Latin
    /// </summary>
    public string? NameLatin { get; set; }

    /// <summary>
    /// Tên viết tắt của quốc gia
    /// </summary>
    public string? ShortName { get; set; }
}
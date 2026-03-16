using FreshX.Domain.Common;

namespace FreshX.Domain.Entities;

public partial class Icdchapter : BaseEntity
{
    public string? Code { get; set; } // Mã chương ICD

    public string? Name { get; set; } // Tên chương ICD

    public string? NameEnglish { get; set; } // Tên chương ICD bằng tiếng Anh

    public string? NameVietNamese { get; set; } // Tên chương ICD bằng tiếng Việt

    public string? NameUnaccented { get; set; } // Tên không dấu của chương ICD
}
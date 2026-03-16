using FreshX.Domain.Common;

namespace FreshX.Domain.Entities;

public partial class ICDCatalog : BaseEntity
{
    public string? Code { get; set; } // Mã danh mục ICD

    public string? Name { get; set; } // Tên danh mục ICD

    public string? NameEnglish { get; set; } // Tên danh mục ICD bằng tiếng Anh

    public string? NameRussian { get; set; } // Tên danh mục ICD bằng tiếng Nga

    public string? Level { get; set; } // Cấp độ danh mục ICD

    public int? ICDCatalogGroupId { get; set; } // ID nhóm danh mục ICD

    public string? Subgroup { get; set; } // Nhóm phụ

    public short? Type { get; set; } // Loại danh mục ICD

    public bool IsInfectious { get; set; } // Trạng thái bệnh truyền nhiễm

    public string? NameUnaccented { get; set; } // Tên không dấu của danh mục ICD

    public int? LegacyCode { get; set; } // Mã kế thừa

    public virtual ICDCatalog? ICDCatalogGroup { get; set; } // Nhóm danh mục ICD
}
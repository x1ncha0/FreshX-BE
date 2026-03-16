using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;

namespace Freshx_API.Models;

public partial class ICDCatalog
{
    public int ICDCatalogId { get; set; } // ID danh mục ICD

    public string? Code { get; set; } // Mã danh mục ICD

    public string? Name { get; set; } // Tên danh mục ICD

    public string? NameEnglish { get; set; } // Tên danh mục ICD bằng tiếng Anh

    public string? NameRussian { get; set; } // Tên danh mục ICD bằng tiếng Nga

    public string? Level { get; set; } // Cấp độ danh mục ICD

    public int?  ICDCatalogGroupId { get; set; } // ID nhóm danh mục ICD

    public string? Subgroup { get; set; } // Nhóm phụ

    public short? Type { get; set; } // Loại danh mục ICD

    public bool IsInfectious { get; set; } // Trạng thái bệnh truyền nhiễm

    public bool? IsSuspended { get; set; } // Trạng thái tạm ngưng

    public string? NameUnaccented { get; set; } // Tên không dấu của danh mục ICD

    public DateTime? CreatedDate { get; set; } // Ngày tạo

    public int? CreatedBy { get; set; } // Người tạo

    public DateTime? UpdatedDate { get; set; } // Ngày cập nhật

    public int? UpdatedBy { get; set; } // Người cập nhật

    public int? LegacyCode { get; set; } // Mã kế thừa
    public virtual  ICDCatalog?  ICDCatalogGroup { get; set; } // Nhóm danh mục ICD
    //Giải thích:
    //ICDCatalogGroup
    //Loại: virtual ICDCatalog?
    //Ý nghĩa: Xác định nhóm danh mục ICD mà danh mục này thuộc về.
    //Foreign Key: ICDCatalogGroupId.
    //Chức năng:
    //Dùng để tạo mối quan hệ tự tham chiếu (self-referencing relationship), nghĩa là mỗi danh mục ICD có thể thuộc một nhóm danh mục cha mà không cần tạo bảng danh mục cha riêng.
    //Ví dụ:
    //Danh mục ICD A00 (dịch tả) thuộc nhóm A (bệnh truyền nhiễm).
    //truy vấn
    //var icdGroup = context.ICDCatalogs
    //.Include(c => c.ICDCatalogGroup)
    //.FirstOrDefault(c => c.ICDCatalogId == id);

}

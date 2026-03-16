using System;
using System.Collections.Generic;

namespace Freshx_API.Models;

public partial class Icdchapter
{
    public int IcdchapterId { get; set; } // ID chương ICD

    public string? Code { get; set; } // Mã chương ICD

    public string? Name { get; set; } // Tên chương ICD

    public string? NameEnglish { get; set; } // Tên chương ICD bằng tiếng Anh

    public string? NameVietNamese { get; set; } // Tên chương ICD bằng tiếng Việt

    public bool IsSuspended { get; set; } // Trạng thái tạm ngưng

    public string? NameUnaccented { get; set; } // Tên không dấu của chương ICD

    public DateTime? CreatedDate { get; set; } // Ngày tạo

    public int? CreatedBy { get; set; } // Người tạo

    public DateTime? UpdatedDate { get; set; } // Ngày cập nhật

    public int? UpdatedBy { get; set; } // Người cập nhật
}

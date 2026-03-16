using System;
using System.Collections.Generic;

namespace Freshx_API.Models;

public partial class DrugType
{
  public int DrugTypeId { get; set; } // ID loại thuốc

    public string? Code { get; set; } // Mã loại thuốc

    public string? Name { get; set; } // Tên loại thuốc

    public int? IsSuspended { get; set; } // Trạng thái tạm ngưng

    public int? IsDeleted { get; set; } // Trạng thái đã xóa

    public DateTime? CreatedDate { get; set; } // Ngày tạo

    public int? CreatedBy { get; set; } // Người tạo

    public DateTime? UpdatedDate { get; set; } // Ngày cập nhật

    public int? UpdatedBy { get; set; } // Người cập nhật

 //   public virtual ICollection<DrugCatalog> DrugCatalogs { get; set; } = new List<DrugCatalog>(); // Danh sách danh mục thuốc liên quan
}
    
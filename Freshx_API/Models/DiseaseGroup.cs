using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Freshx_API.Models;

public partial class DiseaseGroup
{
    [Key]
    public int DiseaseGroupId { get; set; } // Nhóm bệnh

    public string? Code { get; set; } // mã nhóm bệnh { ktra trùng lặp}

    public string? Name { get; set; } // Tên Nhóm bệnh

    public int? IsSuspended { get; set; }

    public int? CreatedBy { get; set; }

    public DateTime? CreatedDate { get; set; }

    public int? UpdatedBy { get; set; }

    public DateTime? UpdatedDate { get; set; }

    public int? IsDeleted { get; set; }
    public ICollection<ICDCatalog>? Catalog = new List<ICDCatalog>(); // danh mục bệnh theo tiêu chuản quốc tế
}

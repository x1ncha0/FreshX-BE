using System;
using System.Collections.Generic;

namespace Freshx_API.Models;

public partial class InventoryType
{
    public int InventoryTypeId { get; set; } // ID loại tồn kho

    public string? Code { get; set; } // Mã loại tồn kho

    public string? Name { get; set; } // Tên loại tồn kho

   // public virtual ICollection<Pharmacy> Pharmacies { get; set; } = new List<Pharmacy>(); // Danh sách nhà thuốc liên quan
}

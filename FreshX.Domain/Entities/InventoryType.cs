using FreshX.Domain.Common;

namespace FreshX.Domain.Entities;

public partial class InventoryType : BaseEntity
{
    public string? Code { get; set; } // Mã loại tồn kho

    public string? Name { get; set; } // Tên loại tồn kho
}
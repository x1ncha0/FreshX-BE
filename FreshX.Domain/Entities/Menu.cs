using FreshX.Domain.Common;

namespace FreshX.Domain.Entities;

public partial class Menu : BaseEntity
{
    public string? Name { get; set; } // Tên menu

    public string? Path { get; set; } // đường dẫn

    public int? ParentMenuId { get; set; } // ID menu cha

    public virtual MenuParent? ParentMenu { get; set; } // Menu cha
}
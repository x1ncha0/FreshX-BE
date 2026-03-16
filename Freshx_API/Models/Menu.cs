using System;
using System.Collections.Generic;

namespace Freshx_API.Models;

public partial class Menu
{
    public int MenuId { get; set; } // ID menu
    public string? Name { get; set; } // Tên menu

    public string? Path { get; set; } // đường dẫn
    public int? ParentMenuId { get; set; } // ID menu cha

    public int? IsSuspended { get; set; } // Trạng thái tạm ngưng

    public DateTime? CreatedDate { get; set; } // Ngày tạo

    public int? CreatedBy { get; set; } // Người tạo

    public DateTime? UpdatedDate { get; set; } // Ngày cập nhật

    public int? UpdatedBy { get; set; } // Người cập nhật

    public int? IsDeleted { get; set; } // Trạng thái đã xóa

    //public virtual ICollection<Menu> InverseParentMenu { get; set; } = new List<Menu>(); // Danh sách menu con

    //public virtual ICollection<MenuPermission> MenuPermissions { get; set; } = new List<MenuPermission>(); // Danh sách quyền truy cập menu

    public virtual MenuParent? ParentMenu { get; set; } // Menu cha
}

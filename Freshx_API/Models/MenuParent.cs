using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Freshx_API.Models;

public partial class MenuParent
{
    [Key]
    public int MenuParentId { get; set; } // ID quyền truy cập menu

    public string? UserType { get; set; } // ID người dùng

}

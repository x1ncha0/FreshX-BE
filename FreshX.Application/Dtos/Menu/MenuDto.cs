namespace FreshX.Application.Dtos.Menu
{
    public class MenuPermissionDto
    {
        public int MenuPermissionId { get; set; }
        public int? UserId { get; set; }
        public int? MenuId { get; set; }
        public MenuDto Menu { get; set; } = null!;
        //public UserDto User { get; set; }
    }

    public class MenuDto
    {
        public int MenuId { get; set; }
        public string Code { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public int? ParentMenuId { get; set; }
    }

}


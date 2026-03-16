namespace Freshx_API.Dtos.Menu
{
    public class MenuPermissionDto
    {
        public int MenuPermissionId { get; set; }
        public int? UserId { get; set; }
        public int? MenuId { get; set; }
        public MenuDto Menu { get; set; }
        //public UserDto User { get; set; }
    }

    public class MenuDto
    {
        public int MenuId { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public int? ParentMenuId { get; set; }
    }

}

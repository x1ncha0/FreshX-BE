namespace Freshx_API.Dtos.DepartmenTypeDtos
{
    //  DTO để trả dữ liệu về clien
    public class DepartmentTypeDto
    {
        public int DepartmentTypeId { get; set; }
        public string? Code { get; set; } // Mã phòng ban
        public string? Name { get; set; } // Tên phòng ban
        public int? IsSuspended { get; set; } // Trạng thái phòng ban
        
    }
}

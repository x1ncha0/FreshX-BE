namespace Freshx_API.Dtos
{
    public class TechnicianDto
    {
        public int TechnicianId { get; set; }
        public string? Name { get; set; }
        public string? IdentityCardNumber { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string? Gender { get; set; }
        public int? AvataId { get; set; } // ảnh
        public string? Address { get; set; }
        public string? PositionName { get; set; }
        public string? DepartmentName { get; set; }
        public string? Email { get; set; }
        public string? PhoneNumber { get; set; }
    }
}

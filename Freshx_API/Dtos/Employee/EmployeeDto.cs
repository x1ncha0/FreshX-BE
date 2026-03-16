namespace Freshx_API.Dtos.Employee
{
    public class EmployeeDto
    {
        public int EmployeeId { get; set; }
        public string? FullName { get; set; }
        public string? EmployeeCode { get; set; }
        public string? IdentityCardNumber { get; set; }
        public DateTime? DateOfBirth {  get; set; }
        public string? Gender { get; set; }
        public int? AvataId { get; set; } // ảnh
        public string? Address { get; set; }
        public string? PositionName { get; set; }
        public string? DepartmentName { get; set; }
        public string? Email {  get; set; }
        public string? PhoneNumber {  get; set; }
    }
}

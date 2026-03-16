namespace Freshx_API.Dtos
{
    public class OnlineAppointmentDto
    {
        public int OnlineAppointmentId { get; set; }
        public string? DoctorName { get; set; }
        public string? DepartmentName { get; set; }
        public DateTime? Date {  get; set; }
        public TimeSpan? StartTime { get; set; }
        public string? ReasonForVisit { get; set; }
    }
}

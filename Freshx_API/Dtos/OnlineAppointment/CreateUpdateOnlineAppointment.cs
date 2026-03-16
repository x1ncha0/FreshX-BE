using System.ComponentModel.DataAnnotations;

namespace Freshx_API.Dtos
{
    public class CreateUpdateOnlineAppointment
    {
        [Required(ErrorMessage = "Bác sĩ là bắt buộc")]
        [Range(1, int.MaxValue, ErrorMessage = "DoctorId phải là số nguyên dương")]
        public int? DoctorId { get; set; }
        [Required(ErrorMessage = "Thời gian hẹn là bắt buộc")]
        [Range(1, int.MaxValue, ErrorMessage = "PositionID phải là số nguyên dương")]
        public int TimeSlotId { get; set; }
        [Required(ErrorMessage = "Ngày không được để trống")]
        [DataType(DataType.Date)]
        public DateTime Date {  get; set; }
        [Required(ErrorMessage = "Lý do khám là bắt buộc")]
        public string? ReasonForVisit { get; set; }

    }
}

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Freshx_API.Models
{
    public class OnlineAppointment
    {
        [Key]
        public int OnlineAppointmentId { get; set; }

        [Required]
        public int? DoctorId { get; set; }

        [ForeignKey("DoctorId")]
        public virtual Doctor Doctor { get; set; }
        [Required]
        public string? AccountId { get; set; } // liên kết đến khóa ngoại của AppUserId
        [ForeignKey("AccountId")]
        public virtual AppUser AppUser { get; set; }

        [Required]
        public int TimeSlotId { get; set; }
        [ForeignKey("TimeSlotId")]
        public virtual TimeSlot TimeSlot { get; set; }

        [Required]
        public DateTime Date { get; set; }

        [Required]
        public string? ReasonForVisit { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? UpdatedAt { get; set; }
        public bool? IsDeleted { get; set; } = false;
    }
}

using FreshX.Domain.Common;

namespace FreshX.Domain.Entities
{
    public class OnlineAppointment : BaseEntity
    {
        public int? DoctorId { get; set; }

        public virtual Doctor? Doctor { get; set; }

        public string? AccountId { get; set; } // liên kết đến khóa ngoại của AppUserId

        public virtual AppUser? AppUser { get; set; }

        public int TimeSlotId { get; set; }

        public virtual TimeSlot? TimeSlot { get; set; }

        public DateTime Date { get; set; }

        public string? ReasonForVisit { get; set; }
    }
}
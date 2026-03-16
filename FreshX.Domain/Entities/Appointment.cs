using FreshX.Domain.Common;

namespace FreshX.Domain.Entities;

public partial class Appointment : BaseEntity
{
    public int? ExaminationId { get; set; }

    public int? ReceptionId { get; set; }

    public int? PatientId { get; set; }

    public DateTime? AppointmentTime { get; set; }

    public DateTime? SentTime { get; set; }

    public virtual Examine? Examination { get; set; }

    public virtual Patient? Patient { get; set; }

    public virtual Reception? Reception { get; set; }
}
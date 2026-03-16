using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Freshx_API.Models;

namespace Freshx_API.Models;

public partial class Appointment
{
    [Key]
    public int AppointmentId { get; set; }

    public int? ExaminationId { get; set; } // Khám bệnh

    public int? ReceptionId { get; set; }

    public int? PatientId { get; set; }

    public DateTime? AppointmentTime { get; set; }

    public DateTime? SentTime { get; set; }

    public virtual Examine? Examination { get; set; }

    public virtual Patient? Patient { get; set; }

    public virtual Reception? Reception { get; set; }
}

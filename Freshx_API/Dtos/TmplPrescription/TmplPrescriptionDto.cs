using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Freshx_API.Dtos.Prescription;

namespace Freshx_API.Dtos.TmplPrescription
{
    public class TmplPrescriptionDto
    {
        public int? PrescriptionId { get; set; } // ID đơn thuốc
        public int? MedicalExaminationId { get; set; } // ID khám bệnh

        [Column(TypeName = "decimal(18, 2)")]
        public decimal? TotalAmount { get; set; } // Tổng số tiền

        public bool? IsPaid { get; set; } // Trạng thái đã thanh toán

        [StringLength(500)]
        public string? Note { get; set; } // Ghi chú chung
        public List<DetailDto>? Details { get; set; }
    }

    public class CreateTmplPrescriptionDto
    {
        public int? MedicalExaminationId { get; set; } // ID khám bệnh

        [Column(TypeName = "decimal(18, 2)")]
        public decimal? TotalAmount { get; set; } // Tổng số tiền

        public bool? IsPaid { get; set; } // Trạng thái đã thanh toán

        [StringLength(500)]
        public string? Note { get; set; } // Ghi chú chung
        public List<CreatePrescriptionDetailDto>? Details { get; set; }

    }
    public class UpdateTmplPrescriptionDto
    {
        public int PrescriptionId { get; set; } // ID đơn thuốc
        public int PrescriptionDetailId { get; set; }
        public int? MedicalExaminationId { get; set; } // ID khám bệnh

        [Column(TypeName = "decimal(18, 2)")]
        public decimal? TotalAmount { get; set; } // Tổng số tiền

        public bool? IsPaid { get; set; } // Trạng thái đã thanh toán

        [StringLength(500)]
        public string? Note { get; set; } // Ghi chú chung
        public List<UpdatePrescriptionDetailDto>? Details { get; set; }
    }
}

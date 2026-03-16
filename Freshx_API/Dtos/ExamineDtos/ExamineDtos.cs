using Freshx_API.Dtos.Prescription;

namespace Freshx_API.Dtos.ExamineDtos
{
    public class CreateExamDto {
        public int? ReceptionId { get; set; } // ID tiếp nhận
        public string? ReasonForVisit { get; set; } // Lý do khám bệnh
        public DateTime? CreatedTime { get; set; } // Thời gian tạo
        public bool IsPaid { get; set; } // Đổi trạng thái khi khám xong
        public int? IsDeleted { get; set; } // Trạng thái đã xóa

    }
    public class ExamineRequestDto
    {
        public int ExamineId { get; set; } // ID Khám bệnh

        public int? ReceptionId { get; set; } // ID tiếp nhận

        public DateTime? CreatedDate { get; set; } // Ngày tạo

        public DateTime? CreatedTime { get; set; } // Thời gian tạo

        public string? RespiratoryRate { get; set; } // Tần số hô hấp

        public string? Bmi { get; set; } // Chỉ số BMI

        public string? Symptoms { get; set; } // Triệu chứng

        public int? ICDCatalogId { get; set; } // ID danh mục ICD

        public int? DiagnosisDictionaryId { get; set; } // Từ điển chẩn đoán khám bệnh
        public string? Diagnosis { get; set; } // Chẩn đoán

        public string? Conclusion { get; set; } // Kết luận

        public string? MedicalAdvice { get; set; } // Lời khuyên y tế
        public int? PrescriptionId { get; set; }    // Toa thuốc
        public int? TemplatePrescriptionId { get; set; } // Toa thuốc mẫu

        public string? CreatedById { get; set; } // Người tạo

        public DateTime? UpdatedDate { get; set; } // Ngày cập nhật

        public int? UpdatedBy { get; set; } // Người cập nhật

        public string? PrescriptionNumber { get; set; } // Số đơn thuốc

        public DateTime? FollowUpAppointment { get; set; } // Ngày hẹn tái khám

        public string? Comorbidities { get; set; } // Bệnh kèm theo

        public string? ComorbidityCodes { get; set; } // Mã bệnh kèm theo

        public string? ComorbidityNames { get; set; } // Tên bệnh kèm theo

        public string? MedicalHistory { get; set; } // Tiền sử bệnh

        public string? ExaminationDetails { get; set; } // Chi tiết khám bệnh

        public string? LabSummary { get; set; } // Tóm tắt xét nghiệm

        public string? TreatmentDetails { get; set; } // Chi tiết điều trị

        public string? FollowUpAppointmentNote { get; set; } // Ghi chú hẹn tái khám

        public string? ReasonForVisit { get; set; } // Lý do khám bệnh

        public bool IsPaid { get; set; } // Đổi trạng thái khi khám xong

        public string? ExaminationNote { get; set; } // Ghi chú khám bệnh

        public int? IsDeleted { get; set; } // Trạng thái đã xóa

        // Thông tin thêm khi khám bệnh
        public double? Temperature { get; set; } // Nhiệt độ cơ thể
        public double? Height { get; set; } // Chiều cao (cm)
        public double? Weight { get; set; } // Cân nặng (kg)
        public double? BloodPressureSystolic { get; set; } // Huyết áp tâm thu
        public double? BloodPressureDiastolic { get; set; } // Huyết áp tâm trương
        public double? HeartRate { get; set; } // Nhịp tim (lần/phút)
        public string? OxygenSaturation { get; set; } // Độ bão hòa oxy (SpO2)
        public string? VisionLeft { get; set; } // Thị lực mắt trái
        public string? VisionRight { get; set; } // Thị lực mắt phải
        public string? SkinCondition { get; set; } // Tình trạng da
        public string? OtherPhysicalFindings { get; set; } // Các phát hiện thể chất khác
        public virtual CreatePrescriptionDto? Prescription { get; set; }


    }

    public class ExamineResponseDto
    {
        public int ExamineId { get; set; } // ID Khám bệnh

        public int? ReceptionId { get; set; } // ID tiếp nhận

        public DateTime? CreatedDate { get; set; } // Ngày tạo

        public DateTime? CreatedTime { get; set; } // Thời gian tạo

        public string? RespiratoryRate { get; set; } // Tần số hô hấp

        public string? Bmi { get; set; } // Chỉ số BMI

        public string? Symptoms { get; set; } // Triệu chứng

        public int? ICDCatalogId { get; set; } // ID danh mục ICD

        public int? DiagnosisDictionaryId { get; set; } // Từ điển chẩn đoán khám bệnh
        public string? Diagnosis { get; set; } // Chẩn đoán

        public string? Conclusion { get; set; } // Kết luận

        public string? MedicalAdvice { get; set; } // Lời khuyên y tế
        public int? PrescriptionId { get; set; }    // Toa thuốc
        public int? TemplatePrescriptionId { get; set; } // Toa thuốc mẫu

        public string? CreatedById { get; set; } // Người tạo

        public DateTime? UpdatedDate { get; set; } // Ngày cập nhật

        public int? UpdatedBy { get; set; } // Người cập nhật

        public string? PrescriptionNumber { get; set; } // Số đơn thuốc

        public DateTime? FollowUpAppointment { get; set; } // Ngày hẹn tái khám

        public string? Comorbidities { get; set; } // Bệnh kèm theo

        public string? ComorbidityCodes { get; set; } // Mã bệnh kèm theo

        public string? ComorbidityNames { get; set; } // Tên bệnh kèm theo

        public string? MedicalHistory { get; set; } // Tiền sử bệnh

        public string? ExaminationDetails { get; set; } // Chi tiết khám bệnh

        public string? LabSummary { get; set; } // Tóm tắt xét nghiệm

        public string? TreatmentDetails { get; set; } // Chi tiết điều trị

        public string? FollowUpAppointmentNote { get; set; } // Ghi chú hẹn tái khám

        public string? ReasonForVisit { get; set; } // Lý do khám bệnh

        public bool IsPaid { get; set; } // Đổi trạng thái khi khám xong

        public string? ExaminationNote { get; set; } // Ghi chú khám bệnh

        public int? IsDeleted { get; set; } // Trạng thái đã xóa

        // Thông tin thêm khi khám bệnh
        public double? Temperature { get; set; } // Nhiệt độ cơ thể
        public double? Height { get; set; } // Chiều cao (cm)
        public double? Weight { get; set; } // Cân nặng (kg)
        public double? BloodPressureSystolic { get; set; } // Huyết áp tâm thu
        public double? BloodPressureDiastolic { get; set; } // Huyết áp tâm trương
        public double? HeartRate { get; set; } // Nhịp tim (lần/phút)
        public string? OxygenSaturation { get; set; } // Độ bão hòa oxy (SpO2)
        public string? VisionLeft { get; set; } // Thị lực mắt trái
        public string? VisionRight { get; set; } // Thị lực mắt phải
        public string? SkinCondition { get; set; } // Tình trạng da
        public string? OtherPhysicalFindings { get; set; } // Các phát hiện thể chất khác
                                                           // Thông tin liên kết
        public ReceptionDto? Reception { get; set; } // Thông tin tiếp nhận
        public PrescriptionDto? Prescriptions { get; set; } // Danh sách đơn thuốc

    }

    public class ExamineOnly
    {
        public int ExamineId { get; set; } // ID Khám bệnh
        public bool IsPaid { get; set; } // Đổi trạng thái khi khám xong
    }

}

namespace Freshx_API.Dtos
{
    public class MedicalServiceRequestDto
    {
        public int MedicalServiceRequestId { get; set; } // ID của yêu cầu dịch vụ y tế

        public DateTime? RequestTime { get; set; } // Thời gian yêu cầu

        // ID của lễ tân
        public int? ReceptionId { get; set; }

        // Số lượng dịch vụ
        public int? Quantity { get; set; }

        // ID của dịch vụ
        public int? ServiceId { get; set; }
        public string? Results { get; set; } // kết quả

        // Tổng số tiền của dịch vụ
        public decimal? ServiceTotalAmount { get; set; }

        //chiết khấu
        public decimal? discount { get; set; }

        // Trạng thái phê duyệt
        public bool? IsApproved { get; set; }

        //phòng thực hiện
        public int? DepartmentId { get; set; }

        // Trạng thái của yêu cầu
        public bool? Status { get; set; }

        // ID của người được giao nhiệm vụ
        public int? AssignedById { get; set; }

        // ID của người tạo yêu cầu
        public int? CreatedBy { get; set; }

        // Ngày tạo yêu cầu
        public DateTime? CreatedDate { get; set; }

        // Người cập nhật yêu cầu
        public string? UpdatedBy { get; set; }

        // Ngày cập nhật yêu cầu
        public DateTime? UpdatedDate { get; set; }


    }

    public class CreateMedicalServiceRequestDto
    {
        public DateTime? RequestTime { get; set; }
        public int? ServiceId { get; set; }
        public int? ReceptionId { get; set; }
        public int? Quantity { get; set; }
        //chiết khấu
        public decimal? discount { get; set; }
        public decimal? ServiceTotalAmount { get; set; }
        //phòng thực hiện
        public int? DepartmentId { get; set; }
        public bool? IsApproved { get; set; }
        public bool? Status { get; set; }
        public int? AssignedById { get; set; }

    }

    public class UpdateMedicalServiceRequestDto
    {
        public DateTime? RequestTime { get; set; }
        public int? ServiceId { get; set; }
        public string? Results { get; set; } // kết quả
        public int? ReceptionId { get; set; }
        public int? Quantity { get; set; }
        //chiết khấu
        public decimal? discount { get; set; }
        public decimal? ServiceTotalAmount { get; set; }
        //phòng thực hiện
        public int? DepartmentId { get; set; }
        public bool? IsApproved { get; set; }
        public bool? Status { get; set; }
        public int? AssignedById { get; set; }

    }
}
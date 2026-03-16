using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Runtime.InteropServices;

namespace Freshx_API.Models;

public partial class ServiceCatalog
{

    [Key]
    public int ServiceCatalogId { get; set; } // ID danh mục dịch vụ

    [StringLength(50)]
    public string? Code { get; set; } // Mã danh mục dịch vụ

    [Required]
    [StringLength(100)]
    public string Name { get; set; } = string.Empty; // Tên danh mục dịch vụ (bắt buộc)

    [Column(TypeName = "decimal(18, 2)")]
    public decimal? Price { get; set; } // Giá dịch vụ

    [StringLength(20)]
    public string? UnitOfMeasure { get; set; } // Đơn vị đo lường

    public bool? ServiceStandardValueId { get; set; } // id giá trị chuẩn

    public int? Level { get; set; } // Cấp độ dịch vụ

    public bool? IsParentService { get; set; } // có phải dịch vụ cha không?

    public int? ParentServiceId { get; set; } // ID dịch vụ cha (nếu  là dịch vụ con của?)

    [ForeignKey("ServiceGroup")]
    public int? ServiceGroupId { get; set; } // ID nhóm dịch vụ
    public int? ServiceTypeId { get; set; } // Id loại dịch vụ 
    public int IsSuspended { get; set; } // Trạng thái tạm ngưng (default = false)
    public string? CreatedBy { get; set; } // Người tạo

    public DateTime? CreatedDate { get; set; } // Ngày tạo

    public string? UpdatedBy { get; set; } // Người cập nhật

    public DateTime? UpdatedDate { get; set; } // Ngày cập nhật

    public int? IsDeleted { get; set; } // Trạng thái đã xóa

    // Quan hệ
    [ForeignKey("ParentServiceId")]
    public virtual ServiceCatalog? ParentService { get; set; } // Dịch vụ cha

    public virtual ServiceGroup? ServiceGroup { get; set; } // Nhóm dịch vụ

    public virtual ICollection<ServiceCatalog> ChildServices { get; set; } = new HashSet<ServiceCatalog>(); // Dịch vụ con

    public virtual ICollection<ServiceStandardValue> ServiceStandardValues { get; set; } = new HashSet<ServiceStandardValue>(); // Giá trị tiêu chuẩn dịch vụ

    [ForeignKey("ServiceTypeId")]
    public virtual ServiceTypes? ServiceTypes { get; set; }
    // giải thích: public virtual ICollection<ServiceCatalog> ChildServices
        // Mục đích:
        //Đại diện cho mối quan hệ Đệ quy Một-Nhiều(self-referencing).
        //Mỗi dịch vụ(ServiceCatalog) có thể là dịch vụ cha, chứa danh sách các dịch vụ con(ChildServices).
        //Cách hoạt động:

        //Một ServiceCatalog có thể có nhiều dịch vụ con khác thông qua ParentServiceId.
        //Trong trường hợp này:
        //ParentServiceId: Trỏ đến ID của dịch vụ cha.
        //ChildServices: Chứa tất cả các dịch vụ con mà dịch vụ hiện tại là cha của chúng.

}

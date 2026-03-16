using FreshX.Domain.Common;

namespace FreshX.Domain.Entities;

public partial class DrugCatalog : BaseEntity
{
    /// <summary>
    /// Mã danh mục thuốc
    /// </summary>
    public string? Code { get; set; }

    /// <summary>
    /// Tên danh mục thuốc
    /// </summary>
    public string? Name { get; set; }

    /// <summary>
    /// ID đơn vị đo lường
    /// </summary>
    public int? UnitOfMeasureId { get; set; }

    /// <summary>
    /// ID nhà sản xuất
    /// </summary>
    public int? ManufacturerId { get; set; }


    /// <summary>
    /// ID quốc gia
    /// </summary>
    public int? CountryId { get; set; }

    /// <summary>
    /// Tên đầy đủ của thuốc
    /// </summary>
    public string? FullName { get; set; }

    /// <summary>
    /// Tên không dấu của thuốc
    /// </summary>
    public string? NameUnaccented { get; set; }

    /// <summary>
    /// Thành phần hoạt chất
    /// </summary>
    public string? ActiveIngredient { get; set; }

    /// <summary>
    /// Cách sử dụng
    /// </summary>
    public string? Usage { get; set; }

    /// <summary>
    /// Liều lượng
    /// </summary>
    public string? Dosage { get; set; }

    /// <summary>
    /// Tác dụng
    /// </summary>
    public string? Effect { get; set; }

    /// <summary>
    /// ID loại thuốc
    /// </summary>
    public int? DrugTypeId { get; set; }

    /// <summary>
    /// Phân loại thuốc
    /// </summary>
    public string? DrugClassification { get; set; }

    /// <summary>
    /// Đường dùng thuốc
    /// </summary>
    public string? RouteOfAdministration { get; set; }


    /// <summary>
    /// Mô tả thuốc
    /// </summary>
    public string? Description { get; set; }

    /// <summary>
    /// Số tham chiếu
    /// </summary>
    public string? ReferenceNumber { get; set; }

    /// <summary>
    /// Ghi chú 1
    /// </summary>
    public string? Note1 { get; set; }

    /// <summary>
    /// Ghi chú 2
    /// </summary>
    public string? Note2 { get; set; }

    /// <summary>
    /// Ghi chú 3
    /// </summary>
    public string? Note3 { get; set; }

    /// <summary>
    /// Ngày bắt đầu
    /// </summary>
    public DateTime? StartDate { get; set; }

    /// <summary>
    /// Ngày kết thúc
    /// </summary>
    public DateTime? EndDate { get; set; }

    /// <summary>
    /// Số lượng nhập khẩu
    /// </summary>
    public decimal? QuantityImported { get; set; }

    /// <summary>
    /// Số lượng tồn kho
    /// </summary>
    public decimal? QuantityInStock { get; set; }

    /// <summary>
    /// Giá vốn
    /// </summary>
    public decimal? CostPrice { get; set; }

    /// <summary>
    /// Giá bán
    /// </summary>
    public decimal? UnitPrice { get; set; }

    /// <summary>
    /// Quốc gia của thuốc
    /// </summary>
    public virtual Country? Country { get; set; }

    /// <summary>
    /// Loại thuốc
    /// </summary>
    public virtual DrugType? DrugType { get; set; }

    /// <summary>
    /// Đơn vị cung cấp. nơi bán
    /// </summary>
    public virtual Supplier? Manufacturer { get; set; }

    /// <summary>
    /// Đơn vị đo lường
    /// </summary>
    public virtual UnitOfMeasure? UnitOfMeasure { get; set; }
}
using FreshX.Domain.Common;

namespace FreshX.Domain.Entities;

public partial class EmailContent : BaseEntity
{
    /// <summary>
    /// Chủ đề email
    /// </summary>
    public string? Subject { get; set; }

    /// <summary>
    /// Nội dung email
    /// </summary>
    public string? Content { get; set; }

    /// <summary>
    /// Ngày gửi email
    /// </summary>
    public DateTime? SentDate { get; set; }

    /// <summary>
    /// ID người gửi
    /// </summary>
    public int? SenderId { get; set; }

    /// <summary>
    /// Người gửi email
    /// </summary>
    public virtual Employee? Sender { get; set; }
}
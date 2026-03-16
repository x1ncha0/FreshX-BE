using System;
using System.Collections.Generic;

namespace Freshx_API.Models;

public partial class EmailContent
{
    public int EmailContentId { get; set; } // ID nội dung email

    public string? Subject { get; set; } // Chủ đề email

    public string? Content { get; set; } // Nội dung email

    public DateTime? SentDate { get; set; } // Ngày gửi email

    public int? SenderId { get; set; } // ID người gửi

    //public virtual ICollection<EmailContentImage> EmailContentImages { get; set; } = new List<EmailContentImage>(); // Danh sách hình ảnh trong nội dung email

    public virtual Employee? Sender { get; set; } // Người gửi email
}

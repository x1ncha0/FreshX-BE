using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;

namespace Freshx_API.Models
{
    public partial class Savefile
    {
        [Key]
        public int Id { get; set; }
        public string? FileName { get; set; }
        public string? FilePath { get; set; }
        public DateTime UploadDate { get; set; }
    }
}

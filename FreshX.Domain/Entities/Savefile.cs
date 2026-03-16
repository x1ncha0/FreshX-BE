using FreshX.Domain.Common;

namespace FreshX.Domain.Entities
{
    public partial class Savefile : BaseEntity
    {
        public string? FileName { get; set; }

        public string? FilePath { get; set; }

        public DateTime UploadDate { get; set; }
    }
}
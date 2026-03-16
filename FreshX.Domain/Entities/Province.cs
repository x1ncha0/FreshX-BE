using FreshX.Domain.Common;

namespace FreshX.Domain.Entities
{
    public partial class Province : BaseEntity
    {
        public string? Code { get; set; }

        public string? Name { get; set; }

        public string? NameEn { get; set; }

        public string? FullName { get; set; }

        public string? FullNameEn { get; set; }

        public string? CodeName { get; set; }

        public int? AdministrativeUnitId { get; set; }

        public int? AdministrativeRegionId { get; set; }

        public virtual ICollection<District>? Districts { get; set; }
    }
}
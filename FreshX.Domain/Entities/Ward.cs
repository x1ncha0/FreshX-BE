using FreshX.Domain.Common;

namespace FreshX.Domain.Entities
{
    public partial class Ward : BaseEntity
    {
        public string? Code { get; set; }

        public string? Name { get; set; }

        public string? NameEn { get; set; }

        public string? FullName { get; set; }

        public string? FullNameEn { get; set; }

        public string? CodeName { get; set; }

        public string? DistrictCode { get; set; }

        public int? AdministrativeUnitId { get; set; }

        public virtual District? District { get; set; }
    }
}
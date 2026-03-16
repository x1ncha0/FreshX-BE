using FreshX.Domain.Common;

namespace FreshX.Domain.Entities
{
    public partial class District : BaseEntity
    {
        public string? Code { get; set; }

        public string? Name { get; set; }

        public string? NameEn { get; set; }

        public string? FullName { get; set; }

        public string? FullNameEn { get; set; }

        public string? CodeName { get; set; }

        public int? CountryId { get; set; }

        public string? ProvinceCode { get; set; }

        public int? AdministrativeUnitId { get; set; }

        /// <summary>
        /// Navigation property
        /// </summary>
        public virtual Province? Province { get; set; }
    }
}
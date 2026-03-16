using FreshX.Domain.Common;

namespace FreshX.Domain.Entities
{
    public class ServiceTypes : BaseEntity
    {
        public string? Code { get; set; }

        public string? Name { get; set; }
    }
}
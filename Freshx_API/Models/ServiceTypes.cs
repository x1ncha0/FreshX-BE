using System.ComponentModel.DataAnnotations;

namespace Freshx_API.Models
{
    public class ServiceTypes
    {
        [Key]
        public int ServiceTypeId { get; set; }
        public string? Code { get; set; }
        public string? Name { get; set; }
       
    }
}

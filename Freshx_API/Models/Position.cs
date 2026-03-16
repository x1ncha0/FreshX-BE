using System.ComponentModel.DataAnnotations;

namespace Freshx_API.Models
{
    public class Position
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
    }
}


using System.ComponentModel.DataAnnotations;

namespace Freshx_API.Dtos.Auth.Role
{
    public class AddingRole
    {
        [Required (ErrorMessage = "Name bắt buộc phải nhập")]
        public string Name { get; set; }
    }
}

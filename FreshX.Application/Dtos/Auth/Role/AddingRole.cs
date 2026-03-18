
using System.ComponentModel.DataAnnotations;

namespace FreshX.Application.Dtos.Auth.Role
{
    public class AddingRole
    {
        [Required (ErrorMessage = "Name bắt buộc phải nhập")]
        public string Name { get; set; } = string.Empty;
    }
}


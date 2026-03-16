using Freshx_API.Models;
using System.ComponentModel.DataAnnotations;

namespace Freshx_API.Services.CommonServices.ValidationService
{
    public class StrongPasswordAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value == null)
                return new ValidationResult("Mật khẩu không được để trống");

            var password = value.ToString();
            var errors = new List<string>();

            if (password.Length < 6)
                errors.Add("Mật khẩu phải có ít nhất 6 ký tự");

            if (!password.Any(char.IsUpper))
                errors.Add("Mật khẩu phải chứa ít nhất 1 chữ in hoa");

            if (!password.Any(char.IsLower))
                errors.Add("Mật khẩu phải chứa ít nhất 1 chữ thường");

            if (!password.Any(char.IsDigit))
                errors.Add("Mật khẩu phải chứa ít nhất 1 số");

            if (!password.Any(ch => !char.IsLetterOrDigit(ch)))
                errors.Add("Mật khẩu phải chứa ít nhất 1 ký tự đặc biệt");

            if (errors.Any())
                return new ValidationResult(string.Join(", ", errors));

            return ValidationResult.Success;
        }
    }
    public class AgeValidationAttribute : ValidationAttribute
    {
        public int MinAge { get; set; }
        public int MaxAge { get; set; }

        public override bool IsValid(object? value)
        {
            if (value == null) return false;

            var age = DateTime.Today.Year - ((DateTime)value).Year;

            // Subtract one year if birthday hasn't occurred this year
            if (((DateTime)value).Date > DateTime.Today.AddYears(-age))
                age--;

            return age >= MinAge && age <= MaxAge;
        }
    }
    // Custom validation attribute for avatar
    public class AvatarValidationAttribute : ValidationAttribute
    {
        public int MaxSizeInMb { get; set; }
        public string[] AllowedExtensions { get; set; } = Array.Empty<string>();

        public override bool IsValid(object? value)
        {
            if (value == null) return true; // Avatar is optional

            var file = value as IFormFile;
            if (file == null) return false;

            // Check file size
            if (file.Length > MaxSizeInMb * 1024 * 1024)
                return false;

            // Check file extension
            var extension = Path.GetExtension(file.FileName).ToLowerInvariant();
            if (!AllowedExtensions.Contains(extension))
                return false;

            // Validate file content type
            var allowedContentTypes = new[]
            {
            "image/jpeg",
            "image/jpg",
            "image/png"
        };

            return allowedContentTypes.Contains(file.ContentType);
        }
    }
   
}

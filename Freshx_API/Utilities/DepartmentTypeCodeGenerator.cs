using Freshx_API.Models;
using System.Text.RegularExpressions;

namespace Freshx_API.Utilities
{
    public static class DepartmentTypeCodeGenerator
    {
        public static string GenerateCode(FreshxDBContext context, string name)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(name))
                    throw new ArgumentException("Tên không được để trống");

                // Chuyển đổi thành không dấu và uppercase
                string normalized = RemoveVietnameseTone(name.ToUpper());

                // Tách các từ
                string[] words = normalized.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

                if (words.Length < 1)
                    throw new ArgumentException("Tên phải có ít nhất một từ");

                // Tạo mã cơ bản
                string baseCode = GenerateBaseCode(words);

                // Kiểm tra mã trong database và tạo mã mới nếu đã tồn tại
                string finalCode = baseCode;
                int counter = 1;

                // Giả sử bảng của bạn có trường Code
                while (context.DepartmentTypes.Any(x => x.Code == finalCode))
                {
                    finalCode = $"{baseCode}{counter}";
                    counter++;
                }

                return finalCode;
            }
            catch (Exception ex)
            {
                // Log error nếu cần
                throw new Exception($"Lỗi khi tạo mã: {ex.Message}", ex);
            }
        }

        private static string GenerateBaseCode(string[] words)
        {  // Trường hợp không có từ nào
            if (words.Length == 0)
                return "XX";

            // Trường hợp chỉ có một từ
            if (words.Length == 1)
            {
                return words[0].Length >= 2 ? words[0].Substring(0, 2) : words[0] + "X";
            }

            // Trường hợp có nhiều từ
            // Lấy chữ cái đầu của mỗi từ
            string code = string.Join("", words.Select(w => w[0]));

            // Nếu chuỗi quá dài, cắt chỉ lấy 4 ký tự
            if (code.Length > 4)
                code = code.Substring(0, 4);

            return code;
        }

        private static string RemoveVietnameseTone(string text)
        {
            string result = text;
            result = Regex.Replace(result, "[áàảãạăắằẳẵặâấầẩẫậ]", "a");
            result = Regex.Replace(result, "[éèẻẽẹêếềểễệ]", "e");
            result = Regex.Replace(result, "[íìỉĩị]", "i");
            result = Regex.Replace(result, "[óòỏõọôốồổỗộơớờởỡợ]", "o");
            result = Regex.Replace(result, "[úùủũụưứừửữự]", "u");
            result = Regex.Replace(result, "[ýỳỷỹỵ]", "y");
            result = Regex.Replace(result, "[đ]", "d");
            return result;
        }

        // Phương thức kiểm tra mã tồn tại
        public static bool IsCodeExists(FreshxDBContext context, string code)
        {
            return context.DepartmentTypes.Any(x => x.Code == code);
        }

        // Phương thức lấy danh sách mã hiện có
        public static List<string> GetExistingCodes(FreshxDBContext context)
        {
            return context.DepartmentTypes
                         .Select(x => x.Code)
                         .ToList();
        }
    }
}

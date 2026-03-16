using Freshx_API.Models;
using Microsoft.EntityFrameworkCore;

namespace Freshx_API.Utilities
{
    public static class GenerateEmployeeCode
    {

        public static async Task<string> GenerateCode(FreshxDBContext context, string prefix = "NVTN")
        {
            if (context == null)
                throw new ArgumentNullException(nameof(context));

            // Lấy mã nhân viên cuối cùng có prefix tương ứng
            var lastEmployee = await context.Employees
                .Where(d => d.EmployeeCode.StartsWith(prefix))
                .OrderByDescending(d => d.EmployeeCode)
                .FirstOrDefaultAsync();

            if (lastEmployee == null)
            {
                // Nếu chưa có nhân viên nào, bắt đầu từ 001
                return $"{prefix}001";
            }

            // Lấy số thứ tự từ mã nhân viên cuối cùng
            string numberPart = lastEmployee.EmployeeCode.Substring(prefix.Length);
            if (int.TryParse(numberPart, out int lastNumber))
            {
                // Tăng số thứ tự lên 1 và format với 3 chữ số
                return $"{prefix}{(lastNumber + 1).ToString("D3")}";
            }

            // Trong trường hợp có lỗi format, bắt đầu lại từ 001
            return $"{prefix}001";
        }
    }
}

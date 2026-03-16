using Microsoft.AspNetCore.SignalR;

namespace Freshx_API.Services.SignalR
{
    public class CustomUserIdProvider : IUserIdProvider
    {
        public string GetUserId(HubConnectionContext connection)
        {
            // Lấy User ID từ claim của người dùng
            return connection.User?.FindFirst("sub")?.Value; // Hoặc "name", "email", tùy thuộc vào claim bạn sử dụng
        }
    }

}

using Microsoft.AspNetCore.SignalR;

namespace Freshx_API.Services.SignalR
{
    public class ChatHub : Hub
    {
        private readonly ChatService _chatService; // Service để quản lý dữ liệu trò chuyện

        public ChatHub(ChatService chatService)
        {
            _chatService = chatService;
        }

        // Gửi tin nhắn đến tất cả người dùng trong một cuộc trò chuyện
        public async Task SendMessage(int conversationId, string user, string message)
        {
            // Lưu tin nhắn vào database
            await _chatService.SaveMessage(conversationId, user, message);

            // Gửi tin nhắn đến tất cả các client tham gia cuộc trò chuyện
            await Clients.Group(conversationId.ToString())
                .SendAsync("ReceiveMessage", user, message);
        }

        // Người dùng gõ phím
        public async Task UserTyping(int conversationId, string user)
        {
            await Clients.Group(conversationId.ToString())
                .SendAsync("UserTyping", user);
        }

        // Tham gia vào một cuộc trò chuyện
        public async Task JoinConversation(int conversationId)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, conversationId.ToString());
            await Clients.Group(conversationId.ToString())
                .SendAsync("UserJoined", Context.User.Identity.Name);
        }

        // Rời khỏi một cuộc trò chuyện
        public async Task LeaveConversation(int conversationId)
        {
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, conversationId.ToString());
            await Clients.Group(conversationId.ToString())
                .SendAsync("UserLeft", Context.User.Identity.Name);
        }

        //gửi thông báo
        public async Task SendNotification(string message)
        {
            await Clients.All.SendAsync("ReceiveNotification", message);
        }
    }

}

using Freshx_API.Models;
using Microsoft.EntityFrameworkCore;
using System;

namespace Freshx_API.Services.SignalR
{
    public class ChatService
    {
        private readonly FreshxDBContext _context;

        public ChatService(FreshxDBContext context)
        {
            _context = context;
        }

        // Lưu tin nhắn vào database
        public async Task SaveMessage(int conversationId, string user, string message)
        {
            var chatMessage = new ChatMessage
            {
                ConversationId = conversationId,
                User = user,
                Message = message,
                Timestamp = DateTime.UtcNow
            };

            _context.ChatMessages.Add(chatMessage);
            await _context.SaveChangesAsync();
        }

        // Lấy danh sách các cuộc trò chuyện
        public async Task<List<Conversation>> GetConversations()
        {
            return await _context.Conversations
                .OrderByDescending(c => c.CreatedAt)
                .ToListAsync();
        }

        // Lấy lịch sử tin nhắn của một cuộc trò chuyện
        public async Task<List<ChatMessage>> GetMessages(int conversationId)
        {
            return await _context.ChatMessages
                .Where(m => m.ConversationId == conversationId)
                .OrderBy(m => m.Timestamp)
                .ToListAsync();
        }

        // Tạo một cuộc trò chuyện mới
        public async Task<int> CreateConversation(string title)
        {
            var conversation = new Conversation
            {
                Title = title,
                CreatedAt = DateTime.UtcNow
            };

            _context.Conversations.Add(conversation);
            await _context.SaveChangesAsync();

            return conversation.Id;
        }
    }

}

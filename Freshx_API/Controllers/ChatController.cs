using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System.Collections.Generic;
using Freshx_API.Services.SignalR;
using Freshx_API.Dtos;

namespace Freshx_API.Controllers
{ 
    [Route("api/[controller]")]
    [ApiController]
    public class ChatController : ControllerBase
    {
        private readonly ChatService _chatService;

        public ChatController(ChatService chatService)
        {
            _chatService = chatService;
        }

        // Lấy danh sách các cuộc trò chuyện
        [HttpGet("conversations")]
        public async Task<IActionResult> GetConversations()
        {
            var conversations = await _chatService.GetConversations();
            return Ok(conversations);
        }

        // Lấy lịch sử tin nhắn theo ConversationId
        [HttpGet("conversations/{conversationId}/messages")]
        public async Task<IActionResult> GetMessages(int conversationId)
        {
            var messages = await _chatService.GetMessages(conversationId);
            if (messages == null)
                return NotFound($"No messages found for conversation ID {conversationId}.");

            return Ok(messages);
        }

        // Tạo một cuộc trò chuyện mới
        [HttpPost("conversations")]
        public async Task<IActionResult> CreateConversation([FromBody] CreateConversationRequest request)
        {
            if (string.IsNullOrEmpty(request.Title))
                return BadRequest("Title is required.");

            var conversationId = await _chatService.CreateConversation(request.Title);
            return CreatedAtAction(nameof(GetMessages), new { conversationId }, new { conversationId });
        }
    }

}

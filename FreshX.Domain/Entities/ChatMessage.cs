using FreshX.Domain.Common;

namespace FreshX.Domain.Entities
{
    public class ChatMessage : BaseEntity
    {
        public int ConversationId { get; set; }

        public string User { get; set; } = string.Empty;

        public string Message { get; set; } = string.Empty;

        public DateTime Timestamp { get; set; }
    }
}
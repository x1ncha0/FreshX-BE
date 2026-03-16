namespace Freshx_API.Models
{
    public class ChatMessage
    {
        public int ChatMessageId { get; set; }
        public int ConversationId { get; set; }
        public string User { get; set; }
        public string Message { get; set; }
        public DateTime Timestamp { get; set; }
    }

}

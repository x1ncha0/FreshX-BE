namespace Freshx_API.Models
{
    public class Conversation
    {
        public int Id { get; set; }
        public string Title { get; set; } // Tên cuộc trò chuyện
        public List<ChatMessage> Messages { get; set; }
        public DateTime CreatedAt { get; set; }
    }

}

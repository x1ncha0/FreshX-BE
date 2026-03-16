using FreshX.Domain.Common;

namespace FreshX.Domain.Entities
{
    public class Conversation : BaseEntity
    {
        /// <summary>
        /// Tên cuộc trò chuyện
        /// </summary>
        public string Title { get; set; } = string.Empty;

        public List<ChatMessage>? Messages { get; set; }
    }
}
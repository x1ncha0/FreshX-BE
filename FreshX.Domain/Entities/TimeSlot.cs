using FreshX.Domain.Common;

namespace FreshX.Domain.Entities
{
    public class TimeSlot : BaseEntity
    {
        public string? Name { get; set; }

        public TimeSpan StartTime { get; set; }

        public TimeSpan EndTime { get; set; }

        public TimeSpan Duration { get; private set; }
    }
}
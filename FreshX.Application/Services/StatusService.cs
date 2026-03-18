using FreshX.Application.Dtos.Status;
using FreshX.Application.Interfaces;

namespace FreshX.Application.Services
{
    public class StatusService : IStatusService
    {
        private static readonly IReadOnlyList<StatusOptionDto> Statuses =
        [
            new StatusOptionDto { ValueId = 0, Name = "Hoạt động" },
            new StatusOptionDto { ValueId = 1, Name = "Tạm ngưng" }
        ];

        public IReadOnlyList<StatusOptionDto> GetAll() => Statuses;
    }
}

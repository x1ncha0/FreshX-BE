using FreshX.Application.Dtos.Status;

namespace FreshX.Application.Interfaces
{
    public interface IStatusService
    {
        IReadOnlyList<StatusOptionDto> GetAll();
    }
}

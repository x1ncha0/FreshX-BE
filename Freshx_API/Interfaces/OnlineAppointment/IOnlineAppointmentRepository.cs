using Freshx_API.Dtos;
using Freshx_API.Models;

namespace Freshx_API.Interfaces
{
    public interface IOnlineAppointmentRepository
    {
        public Task<OnlineAppointment?> CreateOnlineAppointment(CreateUpdateOnlineAppointment request,string accountId);
        public Task<OnlineAppointment?> GetOnlineAppointmentById(string accountId);
        public Task<OnlineAppointment?> UpdateOnlineOppointmentById(int id, CreateUpdateOnlineAppointment request);
        public Task<OnlineAppointment?> DeleteOnlineOppointmentById(int id);
    }
}

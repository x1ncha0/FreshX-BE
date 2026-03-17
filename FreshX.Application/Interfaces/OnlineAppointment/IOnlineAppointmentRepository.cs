using FreshX.Application.Dtos;
using FreshX.Domain.Entities;

namespace FreshX.Application.Interfaces
{
    public interface IOnlineAppointmentRepository
    {
        public Task<OnlineAppointment?> CreateOnlineAppointment(CreateUpdateOnlineAppointment request,string accountId);
        public Task<OnlineAppointment?> GetOnlineAppointmentById(string accountId);
        public Task<OnlineAppointment?> UpdateOnlineOppointmentById(int id, CreateUpdateOnlineAppointment request);
        public Task<OnlineAppointment?> DeleteOnlineOppointmentById(int id);
    }
}


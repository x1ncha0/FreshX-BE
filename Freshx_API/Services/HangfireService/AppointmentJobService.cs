using Freshx_API.Interfaces.Auth;
using Freshx_API.Models;
using Microsoft.EntityFrameworkCore;

namespace Freshx_API.Services.HangfireService
{
    public class AppointmentJobService
    {
        private readonly ILogger<AppointmentJobService> _logger;
        private readonly FreshxDBContext _context;
        private readonly IEmailService _emailService;

        public AppointmentJobService(
            ILogger<AppointmentJobService> logger,
            FreshxDBContext context,
            IEmailService emailService)
        {
            _logger = logger;
            _context = context;
            _emailService = emailService;
        }

        // Cleanup deleted and expired appointments
        public async Task CleanupAppointments()
        {
            try
            {
                var today = DateTime.Today;
                var deletedOrExpiredAppointments = await _context.OnlineAppointments
                    .Where(a => a.IsDeleted == true || a.Date.Date < today)
                    .ToListAsync();

                if (deletedOrExpiredAppointments.Any())
                {
                    _context.OnlineAppointments.RemoveRange(deletedOrExpiredAppointments);
                    await _context.SaveChangesAsync();
                    _logger.LogInformation($"Cleaned up {deletedOrExpiredAppointments.Count} appointments");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error during appointment cleanup");
            }
        }

        // Send email notifications for today's appointments
        public async Task SendAppointmentReminders()
        {
            try
            {
                
                
                var today = DateTime.Today;
                var todayAppointments = await _context.OnlineAppointments
                    .Where(a => a.Date.Date == today && a.IsDeleted == false)
                    .Include(a => a.AppUser).Include(a => a.TimeSlot).Include(a => a.Doctor).ThenInclude(d => d.Department)   // Assuming you have Account navigation property
                    .ToListAsync();

                foreach (var appointment in todayAppointments)
                {
                    if (!string.IsNullOrEmpty(appointment.AppUser?.Email))
                    {
                        var emailBody = $"""
    <html>
    <body style="font-family: Arial, sans-serif; max-width: 600px; margin: 0 auto; padding: 20px;">
        <div style="background-color: #f8f9fa; padding: 20px; border-radius: 10px; margin-bottom: 20px;">
            <h2 style="color: #0d6efd; margin-bottom: 20px; text-align: center;">Thông Báo Lịch Khám</h2>
            
            <p style="margin-bottom: 15px;">Kính gửi <strong>{appointment.AppUser?.UserName}</strong>,</p>
            
            <p style="margin-bottom: 15px;">Phòng khám FreshX xin thông báo lịch khám của bạn trong ngày hôm nay:</p>
            
            <div style="background-color: white; padding: 15px; border-radius: 5px; margin: 20px 0;">
                <p style="margin: 10px 0;"><strong>🏥 Lý do khám:</strong> {appointment.ReasonForVisit}</p>
                <p style="margin: 10px 0;"><strong>🕒 Thời gian:</strong> {appointment.TimeSlot?.StartTime}</p>
                <p style="margin: 10px 0;"><strong>👨‍⚕️ Bác sĩ:</strong> {appointment.Doctor?.Name ?? "Chưa phân công"}</p>
                <p style="margin: 10px 0;"><strong>📍 Địa điểm:</strong> {appointment.Doctor.Department.Name}</p>
            </div>
            
            <p style="margin: 15px 0; color: #6c757d;">Vui lòng đến đúng giờ để được phục vụ tốt nhất.</p>
            
            <div style="background-color: #e7f3ff; padding: 15px; border-radius: 5px; margin-top: 20px;">
                <p style="margin: 0; color: #0d6efd;">
                    <strong>Lưu ý:</strong> Nếu bạn không thể đến khám theo lịch hẹn, 
                    vui lòng thông báo trước để chúng tôi có thể sắp xếp lại lịch khám.
                </p>
            </div>
        </div>
        
        <div style="text-align: center; color: #6c757d; font-size: 14px;">
            <p>Phòng khám FreshX</p>
            <p>Hotline: 1900 xxxx</p>
            <p>Email: contact@freshx.com</p>
        </div>
    </body>
    </html>
    """;
                        await _emailService.SendEmailAsync(
      appointment.AppUser?.Email ?? throw new InvalidOperationException("User email is null"),
      "Bạn có lịch khám ngày hôm nay tại phòng khám FreshX",
      emailBody);
                        _logger.LogInformation($"Sent reminder email for appointment {appointment.AppUser.FullName}");
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error sending appointment reminders");
            }
        }
    }
}

using Hangfire;

namespace Freshx_API.Services.HangfireService
{
    public static class HangfireJobsExtensions
    {
        public static void ConfigureAppointmentJobs(this IApplicationBuilder app)
        {
            // Cleanup job - runs every day at 1 AM
            RecurringJob.AddOrUpdate<AppointmentJobService>(
                "cleanup-appointments",
                job => job.CleanupAppointments(),
               Cron.Minutely());

            // Email reminder job - runs every day at 6 AM
            RecurringJob.AddOrUpdate<AppointmentJobService>(
                "appointment-reminders",
                job => job.SendAppointmentReminders(),
                Cron.Minutely());
        }
    }
}

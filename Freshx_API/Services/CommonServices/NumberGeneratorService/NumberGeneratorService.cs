using Freshx_API.Models;
using Microsoft.EntityFrameworkCore;

namespace Freshx_API.Services.CommonServices
{
    public class NumberGeneratorService
    {
        private readonly FreshxDBContext _context;
        private readonly ILogger<NumberGeneratorService> _logger;

        public NumberGeneratorService(FreshxDBContext context, ILogger<NumberGeneratorService> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<string> GenerateMedicalRecordNumber()
        {
            try
            {
                // Get the last medical record number
                var lastRecord = await _context.Patients
                    .Where(p => p.MedicalRecordNumber != null)
                    .OrderByDescending(p => p.MedicalRecordNumber)
                    .FirstOrDefaultAsync();

                int nextNumber = 1;
                if (lastRecord?.MedicalRecordNumber != null)
                {
                    // Extract number from "bn001" format
                    string numberStr = lastRecord.MedicalRecordNumber.Substring(2);
                    if (int.TryParse(numberStr, out int lastNumber))
                    {
                        nextNumber = lastNumber + 1;
                    }
                }

                return $"bn{nextNumber:D3}"; // Format: bn001, bn002, etc.
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error generating medical record number");
                throw;
            }
        }

        public async Task<string> GenerateAdmissionNumber(DateTime admissionDate)
        {
            try
            {
                int dailyCount = await _context.Patients
           .    CountAsync(p => p.AdmissionNumber != null &&
                          p.CreatedDate.HasValue &&
                          p.CreatedDate.Value.Date == admissionDate.Date);

                dailyCount++; // Increment for new admission

                // Format: nv[YYMMDD][Sequential number - 3 digits]
                string dateStr = admissionDate.ToString("yyMMdd");
                return $"nv{dateStr}{dailyCount:D3}";
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error generating admission number");
                throw;
            }
        }

        public async Task<bool> IsIdentityCardNumberUnique(string identityCardNumber)
        {
            return !await _context.Patients
                .AnyAsync(p => p.IdentityCardNumber == identityCardNumber && p.IsDeleted == 0);
        }
        public async Task<bool> IsEmailUnique(string email)
        {
            return !await _context.Patients
                .AnyAsync(p => p.Email == email && p.IsDeleted == 0);
        }
    }
}

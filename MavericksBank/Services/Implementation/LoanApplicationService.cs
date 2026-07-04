using MavericksBank.DTOs;
using MavericksBank.Models;
using MavericksBank.Repositories.Interfaces;
using MavericksBank.Services.Interfaces;

namespace MavericksBank.Services.Implementation
{
    public class LoanApplicationService : ILoanApplicationService
    {
        private readonly ILoanApplicationRepository _repository;
        private readonly ILogger<LoanApplicationService> _logger;

        public LoanApplicationService(
            ILoanApplicationRepository repository,
            ILogger<LoanApplicationService> logger)
        {
            _repository = repository;
            _logger = logger;
        }

        public async Task<IEnumerable<LoanApplicationDto>> GetAllLoanApplicationsAsync()
        {
            _logger.LogInformation("Fetching all loan applications.");

            var applications = await _repository.GetAllLoanApplicationsAsync();

            return applications.Select(a => new LoanApplicationDto
            {
                LoanApplicationId = a.LoanApplicationId,
                CustomerId = a.CustomerId,
                LoanId = a.LoanId,
                RequestedAmount = a.RequestedAmount,
                Status = a.Status
            });
        }

        public async Task<LoanApplicationDto?> GetLoanApplicationByIdAsync(int id)
        {
            _logger.LogInformation("Fetching loan application with ID: {LoanApplicationId}", id);

            var application = await _repository.GetLoanApplicationByIdAsync(id);

            if (application == null)
            {
                _logger.LogWarning("Loan Application not found. LoanApplicationId: {LoanApplicationId}", id);
                return null;
            }

            return new LoanApplicationDto
            {
                LoanApplicationId = application.LoanApplicationId,
                CustomerId = application.CustomerId,
                LoanId = application.LoanId,
                RequestedAmount = application.RequestedAmount,
                Status = application.Status
            };
        }

        public async Task CreateLoanApplicationAsync(LoanApplicationDto dto)
        {
            var application = new LoanApplication
            {
                CustomerId = dto.CustomerId,
                LoanId = dto.LoanId,
                RequestedAmount = dto.RequestedAmount,
                Status = "Pending"
            };

            await _repository.AddAsync(application);
            await _repository.SaveChangesAsync();

            _logger.LogInformation(
                "Loan application submitted successfully. CustomerId: {CustomerId}, LoanId: {LoanId}",
                application.CustomerId,
                application.LoanId);
        }

        public async Task UpdateLoanApplicationAsync(int id, LoanApplicationDto dto)
        {
            var application = await _repository.GetLoanApplicationByIdAsync(id);

            if (application == null)
            {
                _logger.LogWarning("Update failed. Loan Application not found. LoanApplicationId: {LoanApplicationId}", id);
                throw new Exception("Loan Application not found.");
            }

            application.RequestedAmount = dto.RequestedAmount;

            if (dto.Status.Equals("Pending", StringComparison.OrdinalIgnoreCase) ||
                dto.Status.Equals("Approved", StringComparison.OrdinalIgnoreCase) ||
                dto.Status.Equals("Rejected", StringComparison.OrdinalIgnoreCase))
            {
                application.Status = dto.Status;
            }

            await _repository.UpdateAsync(application);
            await _repository.SaveChangesAsync();

            _logger.LogInformation(
                "Loan application updated successfully. LoanApplicationId: {LoanApplicationId}, Status: {Status}",
                id,
                application.Status);
        }

        public async Task DeleteLoanApplicationAsync(int id)
        {
            var application = await _repository.GetLoanApplicationByIdAsync(id);

            if (application == null)
            {
                _logger.LogWarning("Delete failed. Loan Application not found. LoanApplicationId: {LoanApplicationId}", id);
                throw new Exception("Loan Application not found.");
            }

            await _repository.DeleteAsync(application);
            await _repository.SaveChangesAsync();

            _logger.LogInformation(
                "Loan application deleted successfully. LoanApplicationId: {LoanApplicationId}",
                id);
        }
    }
}
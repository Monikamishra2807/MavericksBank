using MavericksBank.DTOs;
using MavericksBank.Models;
using MavericksBank.Repositories.Interfaces;
using MavericksBank.Services.Interfaces;

namespace MavericksBank.Services.Implementation
{
    public class LoanService : ILoanService
    {
        private readonly ILoanRepository _repository;
        private readonly ILogger<LoanService> _logger;

        public LoanService(
            ILoanRepository repository,
            ILogger<LoanService> logger)
        {
            _repository = repository;
            _logger = logger;
        }

        public async Task<IEnumerable<LoanDto>> GetAllLoansAsync()
        {
            _logger.LogInformation("Fetching all loans.");

            var loans = await _repository.GetAllLoansAsync();

            return loans.Select(l => new LoanDto
            {
                LoanId = l.LoanId,
                LoanName = l.LoanName,
                InterestRate = l.InterestRate,
                TenureInMonths = l.TenureInMonths,
                MaximumAmount = l.MaximumAmount
            });
        }

        public async Task<LoanDto?> GetLoanByIdAsync(int id)
        {
            _logger.LogInformation("Fetching loan with ID: {LoanId}", id);

            var loan = await _repository.GetLoanByIdAsync(id);

            if (loan == null)
            {
                _logger.LogWarning("Loan not found. LoanId: {LoanId}", id);
                return null;
            }

            return new LoanDto
            {
                LoanId = loan.LoanId,
                LoanName = loan.LoanName,
                InterestRate = loan.InterestRate,
                TenureInMonths = loan.TenureInMonths,
                MaximumAmount = loan.MaximumAmount
            };
        }

        public async Task CreateLoanAsync(LoanDto dto)
        {
            var loan = new Loan
            {
                LoanName = dto.LoanName,
                InterestRate = dto.InterestRate,
                TenureInMonths = dto.TenureInMonths,
                MaximumAmount = dto.MaximumAmount
            };

            await _repository.AddAsync(loan);
            await _repository.SaveChangesAsync();

            _logger.LogInformation(
                "Loan created successfully. LoanName: {LoanName}",
                loan.LoanName);
        }

        public async Task UpdateLoanAsync(int id, LoanDto dto)
        {
            var loan = await _repository.GetLoanByIdAsync(id);

            if (loan == null)
            {
                _logger.LogWarning("Update failed. Loan not found. LoanId: {LoanId}", id);
                throw new Exception("Loan not found.");
            }

            loan.LoanName = dto.LoanName;
            loan.InterestRate = dto.InterestRate;
            loan.TenureInMonths = dto.TenureInMonths;
            loan.MaximumAmount = dto.MaximumAmount;

            await _repository.UpdateAsync(loan);
            await _repository.SaveChangesAsync();

            _logger.LogInformation(
                "Loan updated successfully. LoanId: {LoanId}",
                id);
        }

        public async Task DeleteLoanAsync(int id)
        {
            var loan = await _repository.GetLoanByIdAsync(id);

            if (loan == null)
            {
                _logger.LogWarning("Delete failed. Loan not found. LoanId: {LoanId}", id);
                throw new Exception("Loan not found.");
            }

            await _repository.DeleteAsync(loan);
            await _repository.SaveChangesAsync();

            _logger.LogInformation(
                "Loan deleted successfully. LoanId: {LoanId}",
                id);
        }
    }
}
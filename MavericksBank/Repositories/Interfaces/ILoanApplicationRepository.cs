using MavericksBank.Models;

namespace MavericksBank.Repositories.Interfaces
{
    public interface ILoanApplicationRepository
    {
        Task<IEnumerable<LoanApplication>> GetAllLoanApplicationsAsync();
        Task<LoanApplication?> GetLoanApplicationByIdAsync(int id);
        Task AddAsync(LoanApplication loanApplication);
        Task UpdateAsync(LoanApplication loanApplication);
        Task DeleteAsync(LoanApplication loanApplication);
        Task SaveChangesAsync();
    }
}
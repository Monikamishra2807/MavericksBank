using MavericksBank.DTOs;

namespace MavericksBank.Services.Interfaces
{
    public interface ILoanService
    {
        Task<IEnumerable<LoanDto>> GetAllLoansAsync();
        Task<LoanDto?> GetLoanByIdAsync(int id);
        Task CreateLoanAsync(LoanDto dto);
        Task UpdateLoanAsync(int id, LoanDto dto);
        Task DeleteLoanAsync(int id);
    }
}
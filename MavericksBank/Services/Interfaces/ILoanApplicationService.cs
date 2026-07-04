using MavericksBank.DTOs;

namespace MavericksBank.Services.Interfaces
{
    public interface ILoanApplicationService
    {
        Task<IEnumerable<LoanApplicationDto>> GetAllLoanApplicationsAsync();
        Task<LoanApplicationDto?> GetLoanApplicationByIdAsync(int id);
        Task CreateLoanApplicationAsync(LoanApplicationDto dto);
        Task UpdateLoanApplicationAsync(int id, LoanApplicationDto dto);
        Task DeleteLoanApplicationAsync(int id);
    }
}
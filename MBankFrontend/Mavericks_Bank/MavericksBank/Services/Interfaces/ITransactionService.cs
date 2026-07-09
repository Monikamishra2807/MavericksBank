using MavericksBank.DTOs;

namespace MavericksBank.Services.Interfaces
{
    public interface ITransactionService
    {
        Task<IEnumerable<TransactionDto>> GetAllTransactionsAsync();
        Task<TransactionDto?> GetTransactionByIdAsync(int id);
        Task CreateTransactionAsync(TransactionDto dto);
    }
}
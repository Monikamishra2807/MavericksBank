using MavericksBank.Models;

namespace MavericksBank.Repositories.Interfaces
{
    public interface ITransactionRepository
    {
        Task<IEnumerable<Transaction>> GetAllTransactionsAsync();
        Task<Transaction?> GetTransactionByIdAsync(int id);
        Task AddAsync(Transaction transaction);
        Task SaveChangesAsync();
    }
}

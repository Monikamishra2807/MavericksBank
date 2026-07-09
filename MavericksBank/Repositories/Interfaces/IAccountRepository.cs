using MavericksBank.Models;

namespace MavericksBank.Repositories.Interfaces
{
    public interface IAccountRepository
    {
        Task<IEnumerable<Account>> GetAllAccountsAsync();
        Task<Account?> GetAccountByIdAsync(int id);
        Task<Account?> GetAccountByCustomerIdAsync(int customerId);
        Task<Customer?> GetCustomerByUserIdAsync(int userId);
        Task<Account?> GetAccountByNumberAsync(string accountNumber);
        Task AddAsync(Account account);
        Task UpdateAsync(Account account);
        Task DeleteAsync(Account account);
        Task SaveChangesAsync();
    }
}

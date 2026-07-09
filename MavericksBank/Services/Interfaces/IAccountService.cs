using MavericksBank.DTOs;

namespace MavericksBank.Services.Interfaces
{
    public interface IAccountService
    {
        Task<IEnumerable<AccountDto>> GetAllAccountsAsync();
        Task<AccountDto?> GetAccountByIdAsync(int id);
        Task<AccountDto?> GetAccountByCustomerIdAsync(int customerId);
        Task<AccountDto?> GetAccountByNumberAsync(string accountNumber);
        Task CreateAccountAsync(AccountDto dto);
        Task UpdateAccountAsync(int id, AccountDto dto);
        Task DeleteAccountAsync(int id);
    }
}
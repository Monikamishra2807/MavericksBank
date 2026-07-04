using MavericksBank.DTOs;
using MavericksBank.Models;
using MavericksBank.Repositories.Interfaces;
using MavericksBank.Services.Interfaces;

namespace MavericksBank.Services.Implementation
{
    public class AccountService : IAccountService
    {
        private readonly IAccountRepository _repository;
        private readonly ILogger<AccountService> _logger;

        public AccountService(
            IAccountRepository repository,
            ILogger<AccountService> logger)
        {
            _repository = repository;
            _logger = logger;
        }

        public async Task<IEnumerable<AccountDto>> GetAllAccountsAsync()
        {
            _logger.LogInformation("Fetching all accounts.");

            var accounts = await _repository.GetAllAccountsAsync();

            return accounts.Select(a => new AccountDto
            {
                AccountId = a.AccountId,
                CustomerId = a.CustomerId,
                AccountNumber = a.AccountNumber,
                AccountType = a.AccountType,
                IFSCCode = a.IFSCCode,
                BranchName = a.BranchName,
                Balance = a.Balance,
                Status = a.Status,
                IsActive = a.IsActive
            });
        }

        public async Task<AccountDto?> GetAccountByIdAsync(int id)
        {
            _logger.LogInformation("Fetching account with ID: {AccountId}", id);

            var account = await _repository.GetAccountByIdAsync(id);

            if (account == null)
            {
                _logger.LogWarning("Account not found. AccountId: {AccountId}", id);
                return null;
            }

            return new AccountDto
            {
                AccountId = account.AccountId,
                CustomerId = account.CustomerId,
                AccountNumber = account.AccountNumber,
                AccountType = account.AccountType,
                IFSCCode = account.IFSCCode,
                BranchName = account.BranchName,
                Balance = account.Balance,
                Status = account.Status,
                IsActive = account.IsActive
            };
        }

        public async Task CreateAccountAsync(AccountDto dto)
        {
            var account = new Account
            {
                CustomerId = dto.CustomerId,
                AccountNumber = dto.AccountNumber,
                AccountType = dto.AccountType,
                IFSCCode = dto.IFSCCode,
                BranchName = dto.BranchName,
                Balance = dto.Balance,
                Status = dto.Status,
                IsActive = dto.IsActive
            };

            await _repository.AddAsync(account);
            await _repository.SaveChangesAsync();

            _logger.LogInformation(
                "Account created successfully. Account Number: {AccountNumber}",
                account.AccountNumber);
        }

        public async Task UpdateAccountAsync(int id, AccountDto dto)
        {
            var account = await _repository.GetAccountByIdAsync(id);

            if (account == null)
            {
                _logger.LogWarning("Update failed. Account not found. AccountId: {AccountId}", id);
                throw new Exception("Account not found.");
            }

            account.AccountType = dto.AccountType;
            account.IFSCCode = dto.IFSCCode;
            account.BranchName = dto.BranchName;
            account.Balance = dto.Balance;
            account.Status = dto.Status;
            account.IsActive = dto.IsActive;

            await _repository.UpdateAsync(account);
            await _repository.SaveChangesAsync();

            _logger.LogInformation(
                "Account updated successfully. AccountId: {AccountId}",
                id);
        }

        public async Task DeleteAccountAsync(int id)
        {
            var account = await _repository.GetAccountByIdAsync(id);

            if (account == null)
            {
                _logger.LogWarning("Delete failed. Account not found. AccountId: {AccountId}", id);
                throw new Exception("Account not found.");
            }

            await _repository.DeleteAsync(account);
            await _repository.SaveChangesAsync();

            _logger.LogInformation(
                "Account deleted successfully. AccountId: {AccountId}",
                id);
        }
    }
}
using MavericksBank.Data;
using MavericksBank.Models;
using MavericksBank.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace MavericksBank.Repositories.Implementation
{
    public class AccountRepository : IAccountRepository
    {
        private readonly MavericksBankDbContext _context;

        public AccountRepository(MavericksBankDbContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<Account>> GetAllAccountsAsync()
        {
            return await _context.Accounts
                .Include(a => a.Customer)
                .ToListAsync();
        }

        public async Task<Account?> GetAccountByIdAsync(int id)
        {
            return await _context.Accounts
                .Include(a => a.Customer)
                .FirstOrDefaultAsync(a => a.AccountId == id);
        }

        public async Task<Account?> GetAccountByCustomerIdAsync(int customerId)
        {
            return await _context.Accounts
                .FirstOrDefaultAsync(a => a.CustomerId == customerId);
        }

        public async Task<Customer?> GetCustomerByUserIdAsync(int userId)
        {
            return await _context.Customers
                .FirstOrDefaultAsync(c => c.UserId == userId);
        }
        public async Task<Account?> GetAccountByNumberAsync(string accountNumber)
        {
            return await _context.Accounts
                .Include(a => a.Customer)
                .ThenInclude(c => c.User)
                .FirstOrDefaultAsync(a => a.AccountNumber == accountNumber);
        }

        public async Task AddAsync(Account account)
        {
            await _context.Accounts.AddAsync(account);
        }

        public Task UpdateAsync(Account account)
        {
            _context.Accounts.Update(account);
            return Task.CompletedTask;
        }

        public Task DeleteAsync(Account account)
        {
            _context.Accounts.Remove(account);
            return Task.CompletedTask;
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}

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

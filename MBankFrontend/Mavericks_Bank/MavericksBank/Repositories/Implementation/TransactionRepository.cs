using MavericksBank.Data;
using MavericksBank.Models;
using MavericksBank.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace MavericksBank.Repositories.Implementation
{
    public class TransactionRepository : ITransactionRepository
    {
        private readonly MavericksBankDbContext _context;

        public TransactionRepository(MavericksBankDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Transaction>> GetAllTransactionsAsync()
        {
            return await _context.Transactions
                 .Include(t => t.FromAccount)
                 .Include(t => t.ToAccount)
                .ToListAsync();
        }

        public async Task<Transaction?> GetTransactionByIdAsync(int id)
        {
            return await _context.Transactions
                 .Include(t => t.FromAccount)
                 .Include(t => t.ToAccount)
                .FirstOrDefaultAsync(t => t.TransactionId == id);
        }

        public async Task AddAsync(Transaction transaction)
        {
            await _context.Transactions.AddAsync(transaction);
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}

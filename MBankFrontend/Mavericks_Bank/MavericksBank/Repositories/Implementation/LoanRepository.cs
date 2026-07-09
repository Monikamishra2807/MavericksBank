using MavericksBank.Data;
using MavericksBank.Models;
using MavericksBank.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace MavericksBank.Repositories.Implementation
{
    public class LoanRepository : ILoanRepository
    {
        private readonly MavericksBankDbContext _context;

        public LoanRepository(MavericksBankDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Loan>> GetAllLoansAsync()
        {
            return await _context.Loans.ToListAsync();
        }

        public async Task<Loan?> GetLoanByIdAsync(int id)
        {
            return await _context.Loans.FirstOrDefaultAsync(l => l.LoanId == id);
        }

        public async Task AddAsync(Loan loan)
        {
            await _context.Loans.AddAsync(loan);
        }

        public Task UpdateAsync(Loan loan)
        {
            _context.Loans.Update(loan);
            return Task.CompletedTask;
        }

        public Task DeleteAsync(Loan loan)
        {
            _context.Loans.Remove(loan);
            return Task.CompletedTask;
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
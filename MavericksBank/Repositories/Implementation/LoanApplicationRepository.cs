using MavericksBank.Data;
using MavericksBank.Models;
using MavericksBank.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace MavericksBank.Repositories.Implementation
{
    public class LoanApplicationRepository : ILoanApplicationRepository
    {
        private readonly MavericksBankDbContext _context;

        public LoanApplicationRepository(MavericksBankDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<LoanApplication>> GetAllLoanApplicationsAsync()
        {
            return await _context.LoanApplications
                .Include(l => l.Customer)
                .Include(l => l.Loan)
                .ToListAsync();
        }

        public async Task<LoanApplication?> GetLoanApplicationByIdAsync(int id)
        {
            return await _context.LoanApplications
                .Include(l => l.Customer)
                .Include(l => l.Loan)
                .FirstOrDefaultAsync(x => x.LoanApplicationId == id);
        }

        public async Task AddAsync(LoanApplication loanApplication)
        {
            await _context.LoanApplications.AddAsync(loanApplication);
        }

        public Task UpdateAsync(LoanApplication loanApplication)
        {
            _context.LoanApplications.Update(loanApplication);
            return Task.CompletedTask;
        }

        public Task DeleteAsync(LoanApplication loanApplication)
        {
            _context.LoanApplications.Remove(loanApplication);
            return Task.CompletedTask;
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
using MavericksBank.Data;
using MavericksBank.Models;
using MavericksBank.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace MavericksBank.Repositories.Implementation
{
    public class BeneficiaryRepository : IBeneficiaryRepository
    {
        private readonly MavericksBankDbContext _context;

        public BeneficiaryRepository(MavericksBankDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Beneficiary>> GetAllBeneficiariesAsync()
        {
            return await _context.Beneficiaries
                .Include(b => b.Customer)
                .ToListAsync();
        }

        public async Task<Beneficiary?> GetBeneficiaryByIdAsync(int id)
        {
            return await _context.Beneficiaries
                .Include(b => b.Customer)
                .FirstOrDefaultAsync(b => b.BeneficiaryId == id);
        }

        public async Task AddAsync(Beneficiary beneficiary)
        {
            await _context.Beneficiaries.AddAsync(beneficiary);
        }

        public Task UpdateAsync(Beneficiary beneficiary)
        {
            _context.Beneficiaries.Update(beneficiary);
            return Task.CompletedTask;
        }

        public Task DeleteAsync(Beneficiary beneficiary)
        {
            _context.Beneficiaries.Remove(beneficiary);
            return Task.CompletedTask;
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}

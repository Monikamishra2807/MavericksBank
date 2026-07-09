using MavericksBank.Models;

namespace MavericksBank.Repositories.Interfaces
{
    public interface IBeneficiaryRepository
    {
        Task<IEnumerable<Beneficiary>> GetAllBeneficiariesAsync();
        Task<Beneficiary?> GetBeneficiaryByIdAsync(int id);
        Task AddAsync(Beneficiary beneficiary);
        Task UpdateAsync(Beneficiary beneficiary);
        Task DeleteAsync(Beneficiary beneficiary);
        Task SaveChangesAsync();
    }
}

using MavericksBank.DTOs;

namespace MavericksBank.Services.Interfaces
{
    public interface IBeneficiaryService
    {
        Task<IEnumerable<BeneficiaryDto>> GetAllBeneficiariesAsync();
        Task<BeneficiaryDto?> GetBeneficiaryByIdAsync(int id);
        Task CreateBeneficiaryAsync(BeneficiaryDto dto);
        Task UpdateBeneficiaryAsync(int id, BeneficiaryDto dto);
        Task DeleteBeneficiaryAsync(int id);
    }
}
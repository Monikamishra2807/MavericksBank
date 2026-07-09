using MavericksBank.DTOs;
using MavericksBank.Models;
using MavericksBank.Repositories.Interfaces;
using MavericksBank.Services.Interfaces;

namespace MavericksBank.Services.Implementation
{
    public class BeneficiaryService : IBeneficiaryService
    {
        private readonly IBeneficiaryRepository _repository;
        private readonly ILogger<BeneficiaryService> _logger;

        public BeneficiaryService(
            IBeneficiaryRepository repository,
            ILogger<BeneficiaryService> logger)
        {
            _repository = repository;
            _logger = logger;
        }

        public async Task<IEnumerable<BeneficiaryDto>> GetAllBeneficiariesAsync()
        {
            _logger.LogInformation("Fetching all beneficiaries.");

            var beneficiaries = await _repository.GetAllBeneficiariesAsync();

            return beneficiaries.Select(b => new BeneficiaryDto
            {
                BeneficiaryId = b.BeneficiaryId,
                CustomerId = b.CustomerId,
                BeneficiaryName = b.BeneficiaryName,
                BankName = b.BankName,
                AccountNumber = b.AccountNumber,
                IFSCCode = b.IFSCCode
            });
        }

        public async Task<BeneficiaryDto?> GetBeneficiaryByIdAsync(int id)
        {
            _logger.LogInformation("Fetching beneficiary with ID: {BeneficiaryId}", id);

            var beneficiary = await _repository.GetBeneficiaryByIdAsync(id);

            if (beneficiary == null)
            {
                _logger.LogWarning("Beneficiary not found. BeneficiaryId: {BeneficiaryId}", id);
                return null;
            }

            return new BeneficiaryDto
            {
                BeneficiaryId = beneficiary.BeneficiaryId,
                CustomerId = beneficiary.CustomerId,
                BeneficiaryName = beneficiary.BeneficiaryName,
                BankName = beneficiary.BankName,
                AccountNumber = beneficiary.AccountNumber,
                IFSCCode = beneficiary.IFSCCode
            };
        }

        public async Task CreateBeneficiaryAsync(BeneficiaryDto dto)
        {
            var beneficiary = new Beneficiary
            {
                CustomerId = dto.CustomerId,
                BeneficiaryName = dto.BeneficiaryName,
                BankName = dto.BankName,
                AccountNumber = dto.AccountNumber,
                IFSCCode = dto.IFSCCode
            };

            await _repository.AddAsync(beneficiary);
            await _repository.SaveChangesAsync();

            _logger.LogInformation(
                "Beneficiary created successfully. Beneficiary: {BeneficiaryName}",
                beneficiary.BeneficiaryName);
        }

        public async Task UpdateBeneficiaryAsync(int id, BeneficiaryDto dto)
        {
            var beneficiary = await _repository.GetBeneficiaryByIdAsync(id);

            if (beneficiary == null)
            {
                _logger.LogWarning("Update failed. Beneficiary not found. BeneficiaryId: {BeneficiaryId}", id);
                throw new Exception("Beneficiary not found.");
            }

            beneficiary.BeneficiaryName = dto.BeneficiaryName;
            beneficiary.BankName = dto.BankName;
            beneficiary.AccountNumber = dto.AccountNumber;
            beneficiary.IFSCCode = dto.IFSCCode;

            await _repository.UpdateAsync(beneficiary);
            await _repository.SaveChangesAsync();

            _logger.LogInformation(
                "Beneficiary updated successfully. BeneficiaryId: {BeneficiaryId}",
                id);
        }

        public async Task DeleteBeneficiaryAsync(int id)
        {
            var beneficiary = await _repository.GetBeneficiaryByIdAsync(id);

            if (beneficiary == null)
            {
                _logger.LogWarning("Delete failed. Beneficiary not found. BeneficiaryId: {BeneficiaryId}", id);
                throw new Exception("Beneficiary not found.");
            }

            await _repository.DeleteAsync(beneficiary);
            await _repository.SaveChangesAsync();

            _logger.LogInformation(
                "Beneficiary deleted successfully. BeneficiaryId: {BeneficiaryId}",
                id);
        }
    }
}
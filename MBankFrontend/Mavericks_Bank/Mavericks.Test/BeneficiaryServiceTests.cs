using MavericksBank.DTOs;
using MavericksBank.Models;
using MavericksBank.Repositories.Interfaces;
using MavericksBank.Services.Implementation;
using Microsoft.Extensions.Logging.Abstractions;
using Moq;
using NUnit.Framework;

namespace MavericksBank.Test.Services
{
    [TestFixture]
    public class BeneficiaryServiceTests
    {
        private Mock<IBeneficiaryRepository> _repository;
        private BeneficiaryService _service;

        [SetUp]
        public void SetUp()
        {
            _repository = new Mock<IBeneficiaryRepository>();

            _service = new BeneficiaryService(
                _repository.Object,
                NullLogger<BeneficiaryService>.Instance);
        }

        [Test]
        public async Task When_GetAllBeneficiaries_ReturnsBeneficiaryList()
        {
            var beneficiaries = new List<Beneficiary>
            {
                new Beneficiary
                {
                    BeneficiaryId = 1,
                    CustomerId = 1,
                    BeneficiaryName = "Rahul",
                    BankName = "SBI",
                    BranchName = "Chennai",
                    AccountNumber = "1234567890",
                    IFSCCode = "SBIN0001234"
                }
            };

            _repository.Setup(x => x.GetAllBeneficiariesAsync())
                       .ReturnsAsync(beneficiaries);

            var result = await _service.GetAllBeneficiariesAsync();

            Assert.That(result, Is.Not.Null);
            Assert.That(result.Count(), Is.EqualTo(1));
        }

        [Test]
        public async Task When_GetBeneficiaryById_ValidId_ReturnsBeneficiary()
        {
            var beneficiary = new Beneficiary
            {
                BeneficiaryId = 1,
                CustomerId = 1,
                BeneficiaryName = "Rahul",
                BankName = "SBI",
                BranchName = "Chennai",
                AccountNumber = "1234567890",
                IFSCCode = "SBIN0001234"
            };

            _repository.Setup(x => x.GetBeneficiaryByIdAsync(1))
                       .ReturnsAsync(beneficiary);

            var result = await _service.GetBeneficiaryByIdAsync(1);

            Assert.That(result, Is.Not.Null);
            Assert.That(result.BeneficiaryId, Is.EqualTo(1));
        }

        [Test]
        public async Task When_CreateBeneficiary_ValidBeneficiary_CallsRepository()
        {
            var dto = new BeneficiaryDto
            {
                CustomerId = 1,
                BeneficiaryName = "Rahul",
                BankName = "SBI",
                AccountNumber = "1234567890",
                IFSCCode = "SBIN0001234"
            };

            await _service.CreateBeneficiaryAsync(dto);

            _repository.Verify(x => x.AddAsync(It.IsAny<Beneficiary>()), Times.Once);
            _repository.Verify(x => x.SaveChangesAsync(), Times.Once);
        }

        [Test]
        public async Task When_UpdateBeneficiary_ValidBeneficiary_UpdatesRepository()
        {
            var beneficiary = new Beneficiary
            {
                BeneficiaryId = 1,
                CustomerId = 1,
                BeneficiaryName = "Rahul",
                BankName = "SBI",
                AccountNumber = "1234567890",
                IFSCCode = "SBIN0001234"
            };

            var dto = new BeneficiaryDto
            {
                CustomerId = 1,
                BeneficiaryName = "Arun",
                BankName = "HDFC",              
                AccountNumber = "9876543210",
                IFSCCode = "HDFC0001234"
            };

            _repository.Setup(x => x.GetBeneficiaryByIdAsync(1))
                       .ReturnsAsync(beneficiary);

            await _service.UpdateBeneficiaryAsync(1, dto);

            _repository.Verify(x => x.UpdateAsync(It.IsAny<Beneficiary>()), Times.Once);
            _repository.Verify(x => x.SaveChangesAsync(), Times.Once);
        }

        [Test]
        public void When_UpdateBeneficiary_InvalidId_ThrowsException()
        {
            _repository.Setup(x => x.GetBeneficiaryByIdAsync(100))
                       .ReturnsAsync((Beneficiary?)null);

            var ex = Assert.ThrowsAsync<Exception>(async () =>
                await _service.UpdateBeneficiaryAsync(100, new BeneficiaryDto()));

            Assert.That(ex.Message, Is.EqualTo("Beneficiary not found."));
        }

        [Test]
        public async Task When_DeleteBeneficiary_ValidId_DeletesBeneficiary()
        {
            var beneficiary = new Beneficiary
            {
                BeneficiaryId = 1
            };

            _repository.Setup(x => x.GetBeneficiaryByIdAsync(1))
                       .ReturnsAsync(beneficiary);

            await _service.DeleteBeneficiaryAsync(1);

            _repository.Verify(x => x.DeleteAsync(It.IsAny<Beneficiary>()), Times.Once);
            _repository.Verify(x => x.SaveChangesAsync(), Times.Once);
        }
    }
}
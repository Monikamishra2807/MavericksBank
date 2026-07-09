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
    public class LoanApplicationServiceTests
    {
        private Mock<ILoanApplicationRepository> _repository;
        private Mock<ILoanRepository> _loanRepository;
        private LoanApplicationService _service;

        [SetUp]
        public void SetUp()
        {
            _repository = new Mock<ILoanApplicationRepository>();
            _loanRepository = new Mock<ILoanRepository>();

            _service = new LoanApplicationService(
                _repository.Object,
                _loanRepository.Object,
                NullLogger<LoanApplicationService>.Instance);
        }

        [Test]
        public async Task When_GetAllLoanApplications_ReturnsLoanApplicationList()
        {
            var applications = new List<LoanApplication>
            {
                new LoanApplication
                {
                    LoanApplicationId = 1,
                    CustomerId = 1,
                    LoanId = 1,
                    RequestedAmount = 500000,
                    Status = "Pending"
                }
            };

            _repository.Setup(x => x.GetAllLoanApplicationsAsync())
                       .ReturnsAsync(applications);

            var result = await _service.GetAllLoanApplicationsAsync();

            Assert.That(result, Is.Not.Null);
            Assert.That(result.Count(), Is.EqualTo(1));
        }

        [Test]
        public async Task When_GetLoanApplicationById_ValidId_ReturnsLoanApplication()
        {
            var application = new LoanApplication
            {
                LoanApplicationId = 1,
                CustomerId = 1,
                LoanId = 1,
                RequestedAmount = 500000,
                Status = "Pending"
            };

            _repository.Setup(x => x.GetLoanApplicationByIdAsync(1))
                       .ReturnsAsync(application);

            var result = await _service.GetLoanApplicationByIdAsync(1);

            Assert.That(result, Is.Not.Null);
            Assert.That(result.LoanApplicationId, Is.EqualTo(1));
        }

        [Test]
        public async Task When_CreateLoanApplication_ValidApplication_CallsRepository()
        {
            var dto = new LoanApplicationDto
            {
                CustomerId = 1,
                LoanId = 1,
                RequestedAmount = 500000
            };

            _loanRepository.Setup(x => x.GetLoanByIdAsync(1))
                           .ReturnsAsync(new Loan
                           {
                               LoanId = 1,
                               LoanName = "Home Loan",
                               MaximumAmount = 1000000,
                               InterestRate = 8.5m,
                               TenureInMonths = 240
                           });

            await _service.CreateLoanApplicationAsync(dto);

            _repository.Verify(x => x.AddAsync(It.IsAny<LoanApplication>()), Times.Once);
            _repository.Verify(x => x.SaveChangesAsync(), Times.Once);
        }

        [Test]
        public async Task When_UpdateLoanApplication_ValidApplication_UpdatesRepository()
        {
            var application = new LoanApplication
            {
                LoanApplicationId = 1,
                CustomerId = 1,
                LoanId = 1,
                RequestedAmount = 500000,
                Status = "Pending"
            };

            var dto = new LoanApplicationDto
            {
                CustomerId = 1,
                LoanId = 1,
                RequestedAmount = 750000,
                Status = "Approved"
            };

            _repository.Setup(x => x.GetLoanApplicationByIdAsync(1))
                       .ReturnsAsync(application);

            await _service.UpdateLoanApplicationAsync(1, dto);

            _repository.Verify(x => x.UpdateAsync(It.IsAny<LoanApplication>()), Times.Once);
            _repository.Verify(x => x.SaveChangesAsync(), Times.Once);
        }

        [Test]
        public void When_UpdateLoanApplication_InvalidId_ThrowsException()
        {
            _repository.Setup(x => x.GetLoanApplicationByIdAsync(100))
                       .ReturnsAsync((LoanApplication?)null);

            var ex = Assert.ThrowsAsync<Exception>(async () =>
                await _service.UpdateLoanApplicationAsync(100, new LoanApplicationDto()));

            Assert.That(ex.Message, Is.EqualTo("Loan Application not found."));
        }

        [Test]
        public async Task When_DeleteLoanApplication_ValidId_DeletesLoanApplication()
        {
            var application = new LoanApplication
            {
                LoanApplicationId = 1
            };

            _repository.Setup(x => x.GetLoanApplicationByIdAsync(1))
                       .ReturnsAsync(application);

            await _service.DeleteLoanApplicationAsync(1);

            _repository.Verify(x => x.DeleteAsync(It.IsAny<LoanApplication>()), Times.Once);
            _repository.Verify(x => x.SaveChangesAsync(), Times.Once);
        }
    }
}
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
    public class LoanServiceTests
    {
        private Mock<ILoanRepository> _repository;
        private LoanService _service;

        [SetUp]
        public void SetUp()
        {
            _repository = new Mock<ILoanRepository>();

            _service = new LoanService(
                _repository.Object,
                NullLogger<LoanService>.Instance);
        }

        [Test]
        public async Task When_GetAllLoans_ReturnsLoanList()
        {
            var loans = new List<Loan>
            {
                new Loan
                {
                    LoanId = 1,
                    LoanName = "Home Loan",
                    InterestRate = 8.5m,
                    TenureInMonths = 240,
                    MaximumAmount = 5000000
                }
            };

            _repository.Setup(x => x.GetAllLoansAsync())
                       .ReturnsAsync(loans);

            var result = await _service.GetAllLoansAsync();

            Assert.That(result, Is.Not.Null);
            Assert.That(result.Count(), Is.EqualTo(1));
        }

        [Test]
        public async Task When_GetLoanById_ValidId_ReturnsLoan()
        {
            var loan = new Loan
            {
                LoanId = 1,
                LoanName = "Home Loan",
                InterestRate = 8.5m,
                TenureInMonths = 240,
                MaximumAmount = 5000000
            };

            _repository.Setup(x => x.GetLoanByIdAsync(1))
                       .ReturnsAsync(loan);

            var result = await _service.GetLoanByIdAsync(1);

            Assert.That(result, Is.Not.Null);
            Assert.That(result.LoanId, Is.EqualTo(1));
        }

        [Test]
        public async Task When_CreateLoan_ValidLoan_CallsRepository()
        {
            var dto = new LoanDto
            {
                LoanName = "Car Loan",
                InterestRate = 9.5m,
                TenureInMonths = 60,
                MaximumAmount = 1000000
            };

            await _service.CreateLoanAsync(dto);

            _repository.Verify(x => x.AddAsync(It.IsAny<Loan>()), Times.Once);
            _repository.Verify(x => x.SaveChangesAsync(), Times.Once);
        }

        [Test]
        public async Task When_UpdateLoan_ValidLoan_UpdatesRepository()
        {
            var loan = new Loan
            {
                LoanId = 1,
                LoanName = "Home Loan",
                InterestRate = 8.5m,
                TenureInMonths = 240,
                MaximumAmount = 5000000
            };

            var dto = new LoanDto
            {
                LoanName = "Education Loan",
                InterestRate = 7.5m,
                TenureInMonths = 120,
                MaximumAmount = 2000000
            };

            _repository.Setup(x => x.GetLoanByIdAsync(1))
                       .ReturnsAsync(loan);

            await _service.UpdateLoanAsync(1, dto);

            _repository.Verify(x => x.UpdateAsync(It.IsAny<Loan>()), Times.Once);
            _repository.Verify(x => x.SaveChangesAsync(), Times.Once);
        }

        [Test]
        public void When_UpdateLoan_InvalidId_ThrowsException()
        {
            _repository.Setup(x => x.GetLoanByIdAsync(100))
                       .ReturnsAsync((Loan?)null);

            var ex = Assert.ThrowsAsync<Exception>(async () =>
                await _service.UpdateLoanAsync(100, new LoanDto()));

            Assert.That(ex.Message, Is.EqualTo("Loan not found."));
        }

        [Test]
        public async Task When_DeleteLoan_ValidId_DeletesLoan()
        {
            var loan = new Loan
            {
                LoanId = 1
            };

            _repository.Setup(x => x.GetLoanByIdAsync(1))
                       .ReturnsAsync(loan);

            await _service.DeleteLoanAsync(1);

            _repository.Verify(x => x.DeleteAsync(It.IsAny<Loan>()), Times.Once);
            _repository.Verify(x => x.SaveChangesAsync(), Times.Once);
        }
    }
}
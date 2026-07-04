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
    public class AccountServiceTests
    {
        private Mock<IAccountRepository> _repository;
        private AccountService _service;

        [SetUp]
        public void SetUp()
        {
            _repository = new Mock<IAccountRepository>();

            _service = new AccountService(
                _repository.Object,
                NullLogger<AccountService>.Instance);
        }

        [Test]
        public async Task When_GetAllAccounts_ReturnsAccountList()
        {
            var accounts = new List<Account>
            {
                new Account
                {
                    AccountId = 1,
                    CustomerId = 1,
                    AccountNumber = "1234567890",
                    AccountType = "Savings",
                    IFSCCode = "SBIN0001234",
                    BranchName = "Chennai",
                    Balance = 5000,
                    Status = "Active",
                    IsActive = true
                }
            };

            _repository.Setup(x => x.GetAllAccountsAsync())
                       .ReturnsAsync(accounts);

            var result = await _service.GetAllAccountsAsync();

            Assert.That(result, Is.Not.Null);
            Assert.That(result.Count(), Is.EqualTo(1));
        }

        [Test]
        public async Task When_GetAccountById_ValidId_ReturnsAccount()
        {
            var account = new Account
            {
                AccountId = 1,
                CustomerId = 1,
                AccountNumber = "1234567890",
                AccountType = "Savings",
                IFSCCode = "SBIN0001234",
                BranchName = "Chennai",
                Balance = 5000,
                Status = "Active",
                IsActive = true
            };

            _repository.Setup(x => x.GetAccountByIdAsync(1))
                       .ReturnsAsync(account);

            var result = await _service.GetAccountByIdAsync(1);

            Assert.That(result, Is.Not.Null);
            Assert.That(result.AccountId, Is.EqualTo(1));
        }

        [Test]
        public async Task When_CreateAccount_ValidAccount_CallsRepository()
        {
            var dto = new AccountDto
            {
                CustomerId = 1,
                AccountNumber = "1234567890",
                AccountType = "Savings",
                IFSCCode = "SBIN0001234",
                BranchName = "Chennai",
                Balance = 10000,
                Status = "Active",
                IsActive = true
            };

            await _service.CreateAccountAsync(dto);

            _repository.Verify(x => x.AddAsync(It.IsAny<Account>()), Times.Once);
            _repository.Verify(x => x.SaveChangesAsync(), Times.Once);
        }

        [Test]
        public async Task When_UpdateAccount_ValidAccount_UpdatesRepository()
        {
            var account = new Account
            {
                AccountId = 1,
                CustomerId = 1,
                AccountNumber = "1234567890",
                AccountType = "Savings",
                IFSCCode = "SBIN0001234",
                BranchName = "Chennai",
                Balance = 5000,
                Status = "Active",
                IsActive = true
            };

            var dto = new AccountDto
            {
                CustomerId = 1,
                AccountNumber = "1234567890",
                AccountType = "Current",
                IFSCCode = "SBIN0005678",
                BranchName = "Coimbatore",
                Balance = 15000,
                Status = "Active",
                IsActive = true
            };

            _repository.Setup(x => x.GetAccountByIdAsync(1))
                       .ReturnsAsync(account);

            await _service.UpdateAccountAsync(1, dto);

            _repository.Verify(x => x.UpdateAsync(It.IsAny<Account>()), Times.Once);
            _repository.Verify(x => x.SaveChangesAsync(), Times.Once);
        }

        [Test]
        public void When_UpdateAccount_InvalidId_ThrowsException()
        {
            _repository.Setup(x => x.GetAccountByIdAsync(100))
                       .ReturnsAsync((Account?)null);

            var ex = Assert.ThrowsAsync<Exception>(async () =>
                await _service.UpdateAccountAsync(100, new AccountDto()));

            Assert.That(ex.Message, Is.EqualTo("Account not found."));
        }

        [Test]
        public async Task When_DeleteAccount_ValidId_DeletesAccount()
        {
            var account = new Account
            {
                AccountId = 1
            };

            _repository.Setup(x => x.GetAccountByIdAsync(1))
                       .ReturnsAsync(account);

            await _service.DeleteAccountAsync(1);

            _repository.Verify(x => x.DeleteAsync(It.IsAny<Account>()), Times.Once);
            _repository.Verify(x => x.SaveChangesAsync(), Times.Once);
        }
    }
}
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
    public class TransactionServiceTests
    {
        private Mock<ITransactionRepository> _transactionRepository;
        private Mock<IAccountRepository> _accountRepository;
        private TransactionService _service;

        [SetUp]
        public void SetUp()
        {
            _transactionRepository = new Mock<ITransactionRepository>();
            _accountRepository = new Mock<IAccountRepository>();

            _service = new TransactionService(
                _transactionRepository.Object,
                _accountRepository.Object,
                NullLogger<TransactionService>.Instance);
        }

        [Test]
        public async Task When_GetAllTransactions_ReturnsTransactionList()
        {
            var transactions = new List<Transaction>
            {
                new Transaction
                {
                    TransactionId = 1,
                    FromAccountId = 1,
                    ToAccountId = 2,
                    Amount = 500,
                    TransactionType = "Transfer",
                    ReferenceNumber = "REF001",
                    Status = "Success"
                }
            };

            _transactionRepository.Setup(x => x.GetAllTransactionsAsync())
                                  .ReturnsAsync(transactions);

            var result = await _service.GetAllTransactionsAsync();

            Assert.That(result, Is.Not.Null);
            Assert.That(result.Count(), Is.EqualTo(1));
        }

        [Test]
        public async Task When_GetTransactionById_ValidId_ReturnsTransaction()
        {
            var transaction = new Transaction
            {
                TransactionId = 1,
                FromAccountId = 1,
                ToAccountId = 2,
                Amount = 500,
                TransactionType = "Transfer",
                ReferenceNumber = "REF001",
                Status = "Success"
            };

            _transactionRepository.Setup(x => x.GetTransactionByIdAsync(1))
                                  .ReturnsAsync(transaction);

            var result = await _service.GetTransactionByIdAsync(1);

            Assert.That(result, Is.Not.Null);
            Assert.That(result.TransactionId, Is.EqualTo(1));
        }

        [Test]
        public async Task When_CreateTransaction_ValidTransaction_CallsRepository()
        {
            var fromAccount = new Account
            {
                AccountId = 1,
                Balance = 10000
            };

            var toAccount = new Account
            {
                AccountId = 2,
                Balance = 5000
            };

            _accountRepository.Setup(x => x.GetAccountByIdAsync(1))
                              .ReturnsAsync(fromAccount);

            _accountRepository.Setup(x => x.GetAccountByIdAsync(2))
                              .ReturnsAsync(toAccount);

            var dto = new TransactionDto
            {
                FromAccountId = 1,
                ToAccountId = 2,
                Amount = 1000,
                TransactionType = "Transfer"
            };

            await _service.CreateTransactionAsync(dto);

            _transactionRepository.Verify(x => x.AddAsync(It.IsAny<Transaction>()), Times.Once);
            _transactionRepository.Verify(x => x.SaveChangesAsync(), Times.Once);
        }

        [Test]
        public void When_CreateTransaction_InvalidAccount_ThrowsException()
        {
            _accountRepository.Setup(x => x.GetAccountByIdAsync(1))
                              .ReturnsAsync((Account?)null);

            var dto = new TransactionDto
            {
                FromAccountId = 1,
                ToAccountId = 2,
                Amount = 500,
                TransactionType = "Transfer"
            };

            var ex = Assert.ThrowsAsync<Exception>(async () =>
                await _service.CreateTransactionAsync(dto));

            Assert.That(ex.Message, Is.EqualTo("Account not found."));
        }

        [Test]
        public void When_CreateTransaction_InsufficientBalance_ThrowsException()
        {
            var fromAccount = new Account
            {
                AccountId = 1,
                Balance = 100
            };

            var toAccount = new Account
            {
                AccountId = 2,
                Balance = 1000
            };

            _accountRepository.Setup(x => x.GetAccountByIdAsync(1))
                              .ReturnsAsync(fromAccount);

            _accountRepository.Setup(x => x.GetAccountByIdAsync(2))
                              .ReturnsAsync(toAccount);

            var dto = new TransactionDto
            {
                FromAccountId = 1,
                ToAccountId = 2,
                Amount = 500,
                TransactionType = "Transfer"
            };

            var ex = Assert.ThrowsAsync<Exception>(async () =>
                await _service.CreateTransactionAsync(dto));

            Assert.That(ex.Message, Is.EqualTo("Insufficient balance."));
        }
    }
}
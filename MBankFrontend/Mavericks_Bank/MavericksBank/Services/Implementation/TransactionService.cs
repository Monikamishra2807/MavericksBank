using MavericksBank.DTOs;
using MavericksBank.Models;
using MavericksBank.Repositories.Interfaces;
using MavericksBank.Services.Interfaces;

namespace MavericksBank.Services.Implementation
{
    public class TransactionService : ITransactionService
    {
        private readonly ITransactionRepository _transactionRepository;
        private readonly IAccountRepository _accountRepository;
        private readonly ILogger<TransactionService> _logger;

        public TransactionService(
            ITransactionRepository transactionRepository,
            IAccountRepository accountRepository,
            ILogger<TransactionService> logger)
        {
            _transactionRepository = transactionRepository;
            _accountRepository = accountRepository;
            _logger = logger;
        }

        public async Task<IEnumerable<TransactionDto>> GetAllTransactionsAsync()
        {
            _logger.LogInformation("Fetching all transactions.");

            var transactions = await _transactionRepository.GetAllTransactionsAsync();

            return transactions.Select(t => new TransactionDto
            {
                TransactionId = t.TransactionId,
                FromAccountId = t.FromAccountId,
                ToAccountId = t.ToAccountId,
                Amount = t.Amount,
                TransactionType = t.TransactionType,
                ReferenceNumber = t.ReferenceNumber,
                Status = t.Status
            });
        }

        public async Task<TransactionDto?> GetTransactionByIdAsync(int id)
        {
            _logger.LogInformation("Fetching transaction with ID: {TransactionId}", id);

            var transaction = await _transactionRepository.GetTransactionByIdAsync(id);

            if (transaction == null)
            {
                _logger.LogWarning("Transaction not found. TransactionId: {TransactionId}", id);
                return null;
            }

            return new TransactionDto
            {
                TransactionId = transaction.TransactionId,
                FromAccountId = transaction.FromAccountId,
                ToAccountId = transaction.ToAccountId,
                Amount = transaction.Amount,
                TransactionType = transaction.TransactionType,
                ReferenceNumber = transaction.ReferenceNumber,
                Status = transaction.Status
            };
        }

        public async Task CreateTransactionAsync(TransactionDto dto)
        {
            _logger.LogInformation(
                "Transaction initiated. FromAccount: {FromAccountId}, ToAccount: {ToAccountId}, Amount: {Amount}",
                dto.FromAccountId,
                dto.ToAccountId,
                dto.Amount);

            var fromAccount = await _accountRepository.GetAccountByIdAsync(dto.FromAccountId);
            var toAccount = await _accountRepository.GetAccountByIdAsync(dto.ToAccountId);

            if (fromAccount == null || toAccount == null)
            {
                _logger.LogWarning("Transaction failed. One or both accounts not found.");
                throw new Exception("Account not found.");
            }

            if (fromAccount.Balance < dto.Amount)
            {
                _logger.LogWarning(
                    "Transaction failed due to insufficient balance. AccountId: {AccountId}",
                    fromAccount.AccountId);

                throw new Exception("Insufficient balance.");
            }

            fromAccount.Balance -= dto.Amount;
            toAccount.Balance += dto.Amount;

            await _accountRepository.SaveChangesAsync();

            var transaction = new Transaction
            {
                FromAccountId = dto.FromAccountId,
                ToAccountId = dto.ToAccountId,
                Amount = dto.Amount,
                TransactionType = dto.TransactionType,
                ReferenceNumber = Guid.NewGuid().ToString(),
                Status = "Success"
            };

            await _transactionRepository.AddAsync(transaction);
            await _transactionRepository.SaveChangesAsync();

            _logger.LogInformation(
                "Transaction completed successfully. TransactionId: {TransactionId}, Reference: {ReferenceNumber}",
                transaction.TransactionId,
                transaction.ReferenceNumber);
        }
    }
}
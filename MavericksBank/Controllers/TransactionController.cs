using MavericksBank.DTOs;
using MavericksBank.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace MavericksBank.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class TransactionController : ControllerBase
    {
        private readonly ITransactionService _transactionService;
        private readonly ICustomerService _customerService;
        private readonly IAccountService _accountService;

        public TransactionController(
            ITransactionService transactionService,
            ICustomerService customerService,
            IAccountService accountService)
        {
            _transactionService = transactionService;
            _customerService = customerService;
            _accountService = accountService;
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<IActionResult> GetAllTransactions()
        {
            return Ok(await _transactionService.GetAllTransactionsAsync());
        }

        [Authorize(Roles = "Customer,Admin")]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetTransactionById(int id)
        {
            var transaction = await _transactionService.GetTransactionByIdAsync(id);

            if (transaction == null)
                return NotFound();

            return Ok(transaction);
        }

        [Authorize(Roles = "Customer")]
        [HttpPost]
        public async Task<IActionResult> CreateTransaction(TransactionDto dto)
        {
            var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);

            var customer = await _customerService.GetCustomerByUserIdAsync(userId);

            if (customer == null)
            {
                return BadRequest("Please complete your customer profile first.");
            }

            var account = await _accountService.GetAccountByCustomerIdAsync(customer.CustomerId);

            if (account == null)
            {
                return BadRequest("Please open an account first.");
            }

            dto.FromAccountId = account.AccountId;

            await _transactionService.CreateTransactionAsync(dto);

            return Ok(new
            {
                Message = "Transaction completed successfully."
            });
        }
        [Authorize(Roles = "Customer")]
        [HttpGet("test")]
        public IActionResult Test()
        {
            return Ok("Transaction Controller Working");
        }
    }
}
using MavericksBank.DTOs;
using MavericksBank.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MavericksBank.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class TransactionController : ControllerBase
    {
        private readonly ITransactionService _service;

        public TransactionController(ITransactionService service)
        {
            _service = service;
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<IActionResult> GetAllTransactions()
        {
            return Ok(await _service.GetAllTransactionsAsync());
        }

        [Authorize(Roles = "Customer,Admin")]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetTransactionById(int id)
        {
            var transaction = await _service.GetTransactionByIdAsync(id);

            if (transaction == null)
                return NotFound();

            return Ok(transaction);
        }

        [Authorize(Roles = "Customer")]
        [HttpPost]
        public async Task<IActionResult> CreateTransaction(TransactionDto dto)
        {
            await _service.CreateTransactionAsync(dto);

            return Ok(new
            {
                Message = "Transaction completed successfully."
            });
        }
    }
}
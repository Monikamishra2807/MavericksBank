using MavericksBank.DTOs;
using MavericksBank.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MavericksBank.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoanController : ControllerBase
    {
        private readonly ILoanService _service;

        public LoanController(ILoanService service)
        {
            _service = service;
        }

        [Authorize(Roles = "Customer,Admin")]
        [HttpGet]
        public async Task<IActionResult> GetAllLoans()
        {
            return Ok(await _service.GetAllLoansAsync());
        }

        [Authorize(Roles = "Customer,Admin")]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetLoanById(int id)
        {
            var loan = await _service.GetLoanByIdAsync(id);

            if (loan == null)
                return NotFound();

            return Ok(loan);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> CreateLoan(LoanDto dto)
        {
            await _service.CreateLoanAsync(dto);

            return Ok(new
            {
                Message = "Loan created successfully."
            });
        }

        [Authorize(Roles = "Admin")]
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateLoan(int id, LoanDto dto)
        {
            await _service.UpdateLoanAsync(id, dto);

            return Ok(new
            {
                Message = "Loan updated successfully."
            });
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteLoan(int id)
        {
            await _service.DeleteLoanAsync(id);

            return Ok(new
            {
                Message = "Loan deleted successfully."
            });
        }
    }
}
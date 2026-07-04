using MavericksBank.DTOs;
using MavericksBank.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MavericksBank.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class LoanApplicationController : ControllerBase
    {
        private readonly ILoanApplicationService _service;

        public LoanApplicationController(ILoanApplicationService service)
        {
            _service = service;
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<IActionResult> GetAllLoanApplications()
        {
            return Ok(await _service.GetAllLoanApplicationsAsync());
        }

        [Authorize(Roles = "Customer,Admin")]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetLoanApplicationById(int id)
        {
            var application = await _service.GetLoanApplicationByIdAsync(id);

            if (application == null)
                return NotFound();

            return Ok(application);
        }

        [Authorize(Roles = "Customer")]
        [HttpPost]
        public async Task<IActionResult> CreateLoanApplication(LoanApplicationDto dto)
        {
            await _service.CreateLoanApplicationAsync(dto);

            return Ok(new
            {
                Message = "Loan Application submitted successfully."
            });
        }


        [Authorize(Roles = "Admin")]
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateLoanApplication(int id, LoanApplicationDto dto)
        {
            await _service.UpdateLoanApplicationAsync(id, dto);

            return Ok(new
            {
                Message = "Loan Application updated successfully."
            });
        }
        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteLoanApplication(int id)
        {
            await _service.DeleteLoanApplicationAsync(id);

            return Ok(new
            {
                Message = "Loan Application deleted successfully."
            });
        }
    }
}
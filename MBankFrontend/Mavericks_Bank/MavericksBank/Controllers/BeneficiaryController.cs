using MavericksBank.DTOs;
using MavericksBank.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MavericksBank.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BeneficiaryController : ControllerBase
    {
        private readonly IBeneficiaryService _service;

        public BeneficiaryController(IBeneficiaryService service)
        {
            _service = service;
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<IActionResult> GetAllBeneficiaries()
        {
            return Ok(await _service.GetAllBeneficiariesAsync());
        }

        [Authorize(Roles = "Admin,Customer")]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetBeneficiaryById(int id)
        {
            var beneficiary = await _service.GetBeneficiaryByIdAsync(id);

            if (beneficiary == null)
                return NotFound();

            return Ok(beneficiary);
        }

        [Authorize(Roles = "Customer")]
        [HttpPost]
        public async Task<IActionResult> CreateBeneficiary(BeneficiaryDto dto)
        {
            await _service.CreateBeneficiaryAsync(dto);

            return Ok(new
            {
                Message = "Beneficiary created successfully."
            });
        }

        [Authorize(Roles = "Customer")]
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateBeneficiary(int id, BeneficiaryDto dto)
        {
            await _service.UpdateBeneficiaryAsync(id, dto);

            return Ok(new
            {
                Message = "Beneficiary updated successfully."
            });
        }

        [Authorize(Roles = "Customer")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBeneficiary(int id)
        {
            await _service.DeleteBeneficiaryAsync(id);

            return Ok(new
            {
                Message = "Beneficiary deleted successfully."
            });
        }
    }
}
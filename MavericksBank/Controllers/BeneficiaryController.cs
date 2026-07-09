using MavericksBank.DTOs;
using MavericksBank.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace MavericksBank.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BeneficiaryController : ControllerBase
    {
        private readonly IBeneficiaryService _beneficiaryService;
        private readonly ICustomerService _customerService;

        public BeneficiaryController(
            IBeneficiaryService beneficiaryService,
            ICustomerService customerService)
        {
            _beneficiaryService = beneficiaryService;
            _customerService = customerService;
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<IActionResult> GetAllBeneficiaries()
        {
            return Ok(await _beneficiaryService.GetAllBeneficiariesAsync());
        }

        [Authorize(Roles = "Admin,Customer")]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetBeneficiaryById(int id)
        {
            var beneficiary = await _beneficiaryService.GetBeneficiaryByIdAsync(id);

            if (beneficiary == null)
                return NotFound();

            return Ok(beneficiary);
        }

        [Authorize(Roles = "Customer")]
        [HttpPost]
        public async Task<IActionResult> CreateBeneficiary(BeneficiaryDto dto)
        {
            var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);

            var customer = await _customerService.GetCustomerByUserIdAsync(userId);

            if (customer == null)
            {
                return BadRequest("Please complete your customer profile first.");
            }

            dto.CustomerId = customer.CustomerId;

            await _beneficiaryService.CreateBeneficiaryAsync(dto);

            return Ok(new
            {
                Message = "Beneficiary added successfully."
            });
        }

        [Authorize(Roles = "Admin")]
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateBeneficiary(int id, BeneficiaryDto dto)
        {
            await _beneficiaryService.UpdateBeneficiaryAsync(id, dto);

            return Ok(new
            {
                Message = "Beneficiary updated successfully."
            });
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBeneficiary(int id)
        {
            await _beneficiaryService.DeleteBeneficiaryAsync(id);

            return Ok(new
            {
                Message = "Beneficiary deleted successfully."
            });
        }
    }
}
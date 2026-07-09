using MavericksBank.DTOs;
using MavericksBank.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace MavericksBank.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerService _service;

        public CustomerController(ICustomerService service)
        {
            _service = service;
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<IActionResult> GetAllCustomers()
        {
            return Ok(await _service.GetAllCustomersAsync());
        }


        [Authorize(Roles = "Admin,Customer")]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetCustomerById(int id)
        {
            var customer = await _service.GetCustomerByIdAsync(id);

            if (customer == null)
                return NotFound();

            return Ok(customer);
        }
        [Authorize(Roles = "Customer")]
        [HttpGet("MyProfile")]
        public async Task<IActionResult> GetMyProfile()
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (string.IsNullOrEmpty(userId))
            {
                return Unauthorized();
            }

            var customer = await _service.GetCustomerByUserIdAsync(int.Parse(userId));

            if (customer == null)
            {
                return NotFound();
            }

            return Ok(customer);
        }

        [Authorize(Roles = "Customer")]
        [HttpPost]
        public async Task<IActionResult> CreateCustomer(CustomerDto dto)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (string.IsNullOrEmpty(userId))
            {
                return Unauthorized();
            }

            dto.UserId = int.Parse(userId);

            await _service.CreateCustomerAsync(dto);

            return Ok(new
            {
                Message = "Customer created successfully."
            });
        }

        [Authorize(Roles = "Admin,Customer")]
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCustomer(int id, CustomerDto dto)
        {
            await _service.UpdateCustomerAsync(id, dto);

            return Ok(new
            {
                Message = "Customer updated successfully."
            });
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCustomer(int id)
        {
            await _service.DeleteCustomerAsync(id);

            return Ok(new
            {
                Message = "Customer deleted successfully."
            });
        }
    }
}
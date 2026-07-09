using MavericksBank.DTOs;
using MavericksBank.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace MavericksBank.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAccountService _accountService;
        private readonly ICustomerService _customerService;

        public AccountController(
            IAccountService accountService,
            ICustomerService customerService)
        {
            _accountService = accountService;
            _customerService = customerService;
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<IActionResult> GetAllAccounts()
        {
            return Ok(await _accountService.GetAllAccountsAsync());
        }

        [Authorize(Roles = "Admin,Customer")]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetAccountById(int id)
        {
            var account = await _accountService.GetAccountByIdAsync(id);

            if (account == null)
                return NotFound();

            return Ok(account);
        }

        [Authorize(Roles = "Customer")]
        [HttpGet("MyAccount")]
        public async Task<IActionResult> GetMyAccount()
        {
            var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);

            var customer = await _customerService.GetCustomerByUserIdAsync(userId);

            if (customer == null)
                return NotFound("Customer profile not found.");

            var account = await _accountService.GetAccountByCustomerIdAsync(customer.CustomerId);

            if (account == null)
                return NotFound("Account not found.");

            return Ok(account);
        }

        [Authorize(Roles = "Customer")]
        [HttpPost]
        public async Task<IActionResult> CreateAccount(AccountDto dto)
        {
            var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);

            var customer = await _customerService.GetCustomerByUserIdAsync(userId);

            if (customer == null)
                return BadRequest("Please complete your customer profile first.");

            dto.CustomerId = customer.CustomerId;

            await _accountService.CreateAccountAsync(dto);

            return Ok(new
            {
                Message = "Account created successfully."
            });
        }

        [Authorize(Roles = "Admin")]
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAccount(int id, AccountDto dto)
        {
            await _accountService.UpdateAccountAsync(id, dto);

            return Ok(new
            {
                Message = "Account updated successfully."
            });
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAccount(int id)
        {
            await _accountService.DeleteAccountAsync(id);

            return Ok(new
            {
                Message = "Account deleted successfully."
            });
        }
        [AllowAnonymous]
        [HttpGet("AccountNumber/{accountNumber}")]
        public async Task<IActionResult> GetAccountByNumber(string accountNumber)
        {
            var account = await _accountService.GetAccountByNumberAsync(accountNumber);

            if (account == null)
                return NotFound("Account not found.");

            return Ok(account);
        }
    }
}
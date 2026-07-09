using MavericksBank.DTOs;
using MavericksBank.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MavericksBank.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAccountService _service;

        public AccountController(IAccountService service)
        {
            _service = service;
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<IActionResult> GetAllAccounts()
        {
            return Ok(await _service.GetAllAccountsAsync());
        }

        [Authorize(Roles = "Admin,Customer")]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetAccountById(int id)
        {
            var account = await _service.GetAccountByIdAsync(id);

            if (account == null)
                return NotFound();

            return Ok(account);
        }

        [Authorize(Roles = "Customer")]
        [HttpPost]
        public async Task<IActionResult> CreateAccount(AccountDto dto)
        {
            await _service.CreateAccountAsync(dto);

            return Ok(new
            {
                Message = "Account created successfully."
            });
        }

        [Authorize(Roles = "Admin")]
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAccount(int id, AccountDto dto)
        {
            await _service.UpdateAccountAsync(id, dto);

            return Ok(new
            {
                Message = "Account updated successfully."
            });
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAccount(int id)
        {
            await _service.DeleteAccountAsync(id);

            return Ok(new
            {
                Message = "Account deleted successfully."
            });
        }
    }
}
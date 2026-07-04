using MavericksBank.DTOs;
using MavericksBank.Models;
using MavericksBank.Repositories.Interfaces;
using MavericksBank.Services.Interfaces;

namespace MavericksBank.Services.Implementation
{
    public class CustomerService : ICustomerService
    {
        private readonly ICustomerRepository _repository;
        private readonly ILogger<CustomerService> _logger;

        public CustomerService(
            ICustomerRepository repository,
            ILogger<CustomerService> logger)
        {
            _repository = repository;
            _logger = logger;
        }

        public async Task<IEnumerable<CustomerDto>> GetAllCustomersAsync()
        {
            _logger.LogInformation("Fetching all customers.");

            var customers = await _repository.GetAllCustomersAsync();

            return customers.Select(c => new CustomerDto
            {
                CustomerId = c.CustomerId,
                UserId = c.UserId,
                Address = c.Address,
                DOB = c.DOB,
                AadharNumber = c.AadharNumber,
                PanNumber = c.PanNumber
            });
        }

        public async Task<CustomerDto?> GetCustomerByIdAsync(int id)
        {
            _logger.LogInformation("Fetching customer with ID: {CustomerId}", id);

            var customer = await _repository.GetCustomerByIdAsync(id);

            if (customer == null)
            {
                _logger.LogWarning("Customer not found. CustomerId: {CustomerId}", id);
                return null;
            }

            return new CustomerDto
            {
                CustomerId = customer.CustomerId,
                UserId = customer.UserId,
                Address = customer.Address,
                DOB = customer.DOB,
                AadharNumber = customer.AadharNumber,
                PanNumber = customer.PanNumber
            };
        }

        public async Task CreateCustomerAsync(CustomerDto dto)
        {
            var customer = new Customer
            {
                UserId = dto.UserId,
                Address = dto.Address,
                DOB = dto.DOB,
                AadharNumber = dto.AadharNumber,
                PanNumber = dto.PanNumber
            };

            await _repository.AddAsync(customer);
            await _repository.SaveChangesAsync();

            _logger.LogInformation("Customer created successfully. UserId: {UserId}", customer.UserId);
        }

        public async Task UpdateCustomerAsync(int id, CustomerDto dto)
        {
            var customer = await _repository.GetCustomerByIdAsync(id);

            if (customer == null)
            {
                _logger.LogWarning("Update failed. Customer not found. CustomerId: {CustomerId}", id);
                throw new Exception("Customer not found.");
            }

            customer.Address = dto.Address;
            customer.DOB = dto.DOB;
            customer.AadharNumber = dto.AadharNumber;
            customer.PanNumber = dto.PanNumber;

            await _repository.UpdateAsync(customer);
            await _repository.SaveChangesAsync();

            _logger.LogInformation("Customer updated successfully. CustomerId: {CustomerId}", id);
        }

        public async Task DeleteCustomerAsync(int id)
        {
            var customer = await _repository.GetCustomerByIdAsync(id);

            if (customer == null)
            {
                _logger.LogWarning("Delete failed. Customer not found. CustomerId: {CustomerId}", id);
                throw new Exception("Customer not found.");
            }

            await _repository.DeleteAsync(customer);
            await _repository.SaveChangesAsync();

            _logger.LogInformation("Customer deleted successfully. CustomerId: {CustomerId}", id);
        }
    }
}
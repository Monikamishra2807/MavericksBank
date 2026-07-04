using MavericksBank.DTOs;

namespace MavericksBank.Services.Interfaces
{
    public interface ICustomerService
    {
        Task<IEnumerable<CustomerDto>> GetAllCustomersAsync();
        Task<CustomerDto?> GetCustomerByIdAsync(int id);
        Task CreateCustomerAsync(CustomerDto dto);
        Task UpdateCustomerAsync(int id, CustomerDto dto);
        Task DeleteCustomerAsync(int id);
    }
}
using MavericksBank.DTOs;

namespace MavericksBank.Services.Interfaces
{
    public interface ICustomerService
    {
        Task<IEnumerable<CustomerDto>> GetAllCustomersAsync();
        Task<CustomerDto?> GetCustomerByIdAsync(int id);
        Task<CustomerDto?> GetCustomerByUserIdAsync(int userId);
        Task CreateCustomerAsync(CustomerDto dto);
        Task UpdateCustomerAsync(int id, CustomerDto dto);
        Task DeleteCustomerAsync(int id);
    }
}
using MavericksBank.Data;
using MavericksBank.Models;
using MavericksBank.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
namespace MavericksBank.Repositories.Implementation
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly MavericksBankDbContext _Context;
        public CustomerRepository(MavericksBankDbContext context)
        {
            _Context = context;
        }
        public async Task<IEnumerable<Customer>> GetAllCustomersAsync()
        {
            return await _Context.Customers.Include(c => c.User).ToListAsync();
        }
        public async Task<Customer?> GetCustomerByIdAsync(int Id)
        {
            return await _Context.Customers.Include(c => c.User).FirstOrDefaultAsync(c => c.CustomerId == Id);
        }
        public async Task AddAsync(Customer customer)
        {
            await _Context.Customers.AddAsync(customer);
        }
        public  Task UpdateAsync(Customer customer)
        {
            _Context.Customers.Update(customer);
            return Task.CompletedTask;
        }
        public  Task DeleteAsync(Customer customer)
        {
            _Context.Customers.Remove(customer);
            return Task.CompletedTask;
        }
        public async Task SaveChangesAsync()
        {
            await _Context.SaveChangesAsync();
        }
    }
}

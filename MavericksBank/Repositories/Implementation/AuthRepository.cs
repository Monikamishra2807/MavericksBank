using MavericksBank.Data;
using MavericksBank.Models;
using Microsoft.EntityFrameworkCore;
using MavericksBank.Repositories.Interfaces;

namespace MavericksBank.Repositories.Implementation
{
    public class AuthRepository : IAuthRepository
    {
        private readonly MavericksBankDbContext _context;
        public AuthRepository(MavericksBankDbContext context)
        {
            _context = context;
        }
        public async Task<User?> GetUserByEmailAsync(string email)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
        }
        public async Task AddUserAsync(User user)
        {
            await _context.Users.AddAsync(user);
        }
        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}

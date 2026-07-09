namespace MavericksBank.Models
{
    public class User
    {
        public int UserId { get; set; }
        public string FullName { get; set; }= string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; }= string.Empty;
        public string Mobile { get; set; } = string.Empty;
        public string Role { get; set; } = string.Empty;
        public bool IsActive { get; set; } = true;
        public Customer Customer { get; set; }
    }
}

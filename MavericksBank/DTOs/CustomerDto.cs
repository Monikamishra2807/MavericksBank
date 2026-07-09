namespace MavericksBank.DTOs
{
    public class CustomerDto
    {
        public int CustomerId { get; set; }
        public int UserId { get; set; }
        public string Address { get; set; } = string.Empty;
        public DateTime DOB { get; set; }
        public string AadharNumber { get; set; } = string.Empty;
        public string PanNumber { get; set; } = string.Empty;
        public string FullName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Mobile { get; set; } = string.Empty;
    }
}
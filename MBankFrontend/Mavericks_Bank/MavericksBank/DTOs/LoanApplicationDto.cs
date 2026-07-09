namespace MavericksBank.DTOs
{
    public class LoanApplicationDto
    {
        public int LoanApplicationId { get; set; }
        public int CustomerId { get; set; }
        public int LoanId { get; set; }
        public decimal RequestedAmount { get; set; }
        public string Status { get; set; } = string.Empty;
    }
}
namespace MavericksBank.Models
{
    public class LoanApplication
    {
        public int LoanApplicationId { get; set; }
        public int LoanId { get; set; }
        public int CustomerId { get; set; }
        public decimal RequestedAmount { get; set; }
        public string purpose { get; set; } = string.Empty;
        public string Status { get; set; } = string.Empty;
        public Customer Customer { get; set; } = null!;
        public Loan Loan { get; set; } = null!;
    }
}

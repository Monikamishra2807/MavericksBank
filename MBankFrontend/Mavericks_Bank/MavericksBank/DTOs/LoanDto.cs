namespace MavericksBank.DTOs
{
    public class LoanDto
    {
        public int LoanId { get; set; }
        public string LoanName { get; set; } = string.Empty;
        public decimal InterestRate { get; set; }
        public int TenureInMonths { get; set; }
        public decimal MaximumAmount { get; set; }
    }
}
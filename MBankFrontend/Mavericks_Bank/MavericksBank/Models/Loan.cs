namespace MavericksBank.Models
{
    public class Loan
    {
        public int LoanId { get; set; }
        public string LoanName { get; set; } = string.Empty;
        public decimal InterestRate { get; set; }
        public int TenureInMonths { get; set; }
        public decimal MaximumAmount { get; set; }
        public ICollection<LoanApplication> LoanApplications { get; set; } = new List<LoanApplication>();
    }
}

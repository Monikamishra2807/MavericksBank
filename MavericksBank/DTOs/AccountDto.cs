namespace MavericksBank.DTOs
{
    public class AccountDto
    {
        public int AccountId { get; set; }
        public int CustomerId { get; set; }
        public string AccountNumber { get; set; } = string.Empty;
        public string AccountType { get; set; } = string.Empty;
        public string IFSCCode { get; set; } = string.Empty;
        public string BranchName { get; set; } = string.Empty;
        public decimal Balance { get; set; }
        public string Status { get; set; } = string.Empty;
        public bool IsActive { get; set; }
    }
}
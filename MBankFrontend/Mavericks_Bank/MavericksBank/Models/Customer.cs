namespace MavericksBank.Models
{
    public class Customer
    {
        public int CustomerId { get; set; }
        public int UserId { get; set; }
        public DateTime DOB {  get; set; }
        public string AadharNumber { get; set; } = string.Empty;
        public string PanNumber { get; set; } = string.Empty ;
        public string Address { get; set; } = string.Empty;
        public User User { get; set; } = null;
        public ICollection<Account> Accounts { get; set; } = new List<Account>();
        public ICollection<Beneficiary> Beneficiaries { get; set; } = new List<Beneficiary>();
        public ICollection<LoanApplication> LoanApplications { get; set; } = new List<LoanApplication>();
    }
}

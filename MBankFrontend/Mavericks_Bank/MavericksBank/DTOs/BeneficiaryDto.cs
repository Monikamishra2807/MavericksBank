namespace MavericksBank.DTOs
{
    public class BeneficiaryDto
    {
        public int BeneficiaryId { get; set; }
        public int CustomerId { get; set; }
        public string BeneficiaryName { get; set; } = string.Empty;
        public string BankName { get; set; } = string.Empty;
        public string AccountNumber { get; set; } = string.Empty;
        public string IFSCCode { get; set; } = string.Empty;
    }
}
namespace MavericksBank.DTOs
{
    public class TransferDto
    {
        public int FromAccountId { get; set; }
        public int BeneficiaryId { get; set; }
        public decimal Amount { get; set; }
    }
}

    namespace MavericksBank.Models
    {
        public class Transaction
        {
            public int TransactionId { get; set; }
            public int FromAccountId { get; set; }
            public int ToAccountId { get; set; }
            public string TransactionType { get; set; } = string.Empty;
            public decimal Amount { get; set; }
            public string ReferenceNumber { get; set; } = string.Empty;
            public string Status { get; set;  }  = string.Empty;
            public Account FromAccount { get; set; } = null;
            public Account ToAccount { get; set; } = null;
        }
    }

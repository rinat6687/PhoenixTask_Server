namespace PhoenixTaskApp.ViewModels.AccountTransactions
{
    public class AccountTransactionResponse
    {
        public int TransactionId { get; set; }
        public string FullName { get; set; }
        public string UserId { get; set; }
        public int OperationId { get; set; }
        public string OperationDesc { get; set; }
        public string AccountNumber { get; set; }
        public decimal Amount { get; set; }
        public DateTime? TransactionDate { get; set; }

    }
}

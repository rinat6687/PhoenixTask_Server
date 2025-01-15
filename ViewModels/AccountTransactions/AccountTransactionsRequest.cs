namespace PhoenixTaskApp.ViewModels.AccountTransactions
{
    public class AccountTransactionsRequest
    {
        public int TransactionId { get; set; }
        public string FullName { get; set; }
        public string EnglishFullName { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string UserId { get; set; }
        public int OperationId { get; set; }
        public string AccountNumber { get; set; }
        public decimal Amount { get; set; }


    }


}

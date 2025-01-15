namespace PhoenixTaskApp.Core.BusinessEntities.AccountTransaction
{
    public class TransactionsAndStatusData
    {
        public int Code { get; set; }
        public string Status { get; set; }
        public List<AccountTransactionData>? Transactions { get; set; }

    }
}

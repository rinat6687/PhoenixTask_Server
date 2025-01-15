using PhoenixTaskApp.Core.BusinessEntities.AccountTransaction;

namespace PhoenixTaskApp.ViewModels.AccountTransactions
{
    public class TransactionsAndStatusResponse
    {
        public int Code { get; set; }
        public string Status { get; set; }  //todo: ckeck if needed
        public List<AccountTransactionResponse>? Transactions { get; set; }
    }
}

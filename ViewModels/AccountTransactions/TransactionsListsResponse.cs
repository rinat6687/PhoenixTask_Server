using PhoenixTaskApp.Core.BusinessEntities;
using PhoenixTaskApp.ViewModels.OperationsType;

namespace PhoenixTaskApp.ViewModels.AccountTransactions
{
    public class TransactionsListsResponse
    {
        public List<OperationsTypeResponse> OperationsTypeList { get; set; }
    }
}

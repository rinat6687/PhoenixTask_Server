using AutoMapper;
using PhoenixTaskApp.Core.BusinessEntities.AccountTransaction;
using PhoenixTaskApp.Core.BusinessEntities.OperationsType;
using PhoenixTaskApp.ViewModels.AccountTransactions;
using PhoenixTaskApp.ViewModels.OperationsType;

namespace PhoenixTaskApp.Mapping
{
    public class AccountTransactionMappingProfile : Profile
    {

        public AccountTransactionMappingProfile()
        {
            CreateMap<AccountTransactionsRequest, AccountTransactionData>();
            CreateMap<TransactionsListsData, TransactionsListsResponse>();
            CreateMap<OperationsTypeData, OperationsTypeResponse>();
            CreateMap<AccountTransactionData, AccountTransactionResponse>();
            CreateMap<TransactionsAndStatusData, TransactionsAndStatusResponse>();
       

        }
    }
}

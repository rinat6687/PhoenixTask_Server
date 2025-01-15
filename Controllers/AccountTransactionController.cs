using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PhoenixTaskApp.Core.BusinessEntities.AccountTransaction;
using PhoenixTaskApp.Core.BusinessObjects;
using PhoenixTaskApp.ViewModels.AccountTransactions;

namespace PhoenixTaskApp.Controllers
{
    [ApiController]
    [Route("api/accountTransaction")]
    public class AccountTransactionController : ControllerBase
    {
        private readonly BoAccountTransaction _boAccountTransaction;
        private readonly IMapper _mapper;

        public AccountTransactionController(BoAccountTransaction boAccountTransaction, IMapper mapper)
        {
            _boAccountTransaction = boAccountTransaction;
            _mapper = mapper;
        }

        [HttpGet("getTransactionsLists")]
        public async Task<OkObjectResult> GetTransactionsLists()
        {
            var ret = await _boAccountTransaction.GetTransactionsListsAsync();
            return  Ok(_mapper.Map<TransactionsListsResponse>(ret));
        }

        [HttpPost("add")]
        public async Task<OkObjectResult> AddTransaction(AccountTransactionsRequest request)
        {
            var data = _mapper.Map<AccountTransactionData>(request);
            var ret = await _boAccountTransaction.AddTransactionAsync(data);
            return Ok(_mapper.Map<TransactionsAndStatusResponse>(ret));           
        }

        [HttpPost("delete/{transactionId}")]
        public async Task<OkObjectResult> DeleteTransaction(int transactionId)
        {
            var ret = await _boAccountTransaction.DeleteTransactionAsync(transactionId);
            return Ok(_mapper.Map<TransactionsAndStatusResponse>(ret));
        }
    }
}

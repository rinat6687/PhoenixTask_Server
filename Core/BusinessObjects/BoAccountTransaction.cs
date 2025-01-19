using Microsoft.EntityFrameworkCore;
using PhoenixTaskApp.Data.Context;
using PhoenixTaskApp.Data.Modals;
using PhoenixTaskApp.Core.BusinessEntities.Global;
using PhoenixTaskApp.Services;
using PhoenixTaskApp.Core.BusinessEntities.AccountTransaction;
using PhoenixTaskApp.Core.BusinessEntities.OperationsType;

namespace PhoenixTaskApp.Core.BusinessObjects
{

    public class BoAccountTransaction
    {
        private readonly AppDbContext _context;
        private readonly BankService _bankService;

        public BoAccountTransaction(AppDbContext context, BankService bankService)
        {
            _context = context;
            _bankService = bankService;
        }

        //get screen lists
        public async Task<TransactionsListsData> GetTransactionsListsAsync()
        {
            TransactionsListsData transactionsListsData = new TransactionsListsData();


            transactionsListsData.OperationsTypeList = await _context.OperationTypes
                                                        .Select(at => new OperationsTypeData
                                                        {
                                                            OperationId = at.OperationId,
                                                            OperationDesc = at.OperationDesc
                                                        })
                                                        .ToListAsync();

            return transactionsListsData;
        }


        //add transaction
        public async Task<TransactionsAndStatusData> AddTransactionAsync(AccountTransactionData accountTransactionData)
        {
            // add transaction to bank
            ApiResponse response = await _bankService.AddBankTransactionAsync(accountTransactionData);

            // check if success
            if (response.Code >= 200 && response.Code <= 299)
            {
                // add transaction to db
                AccountTransactions accountTransactions = new AccountTransactions
                {
                    FullName = accountTransactionData.FullName,
                    EnglishFullName = accountTransactionData.EnglishFullName,
                    DateOfBirth = accountTransactionData.DateOfBirth,
                    UserId = accountTransactionData.UserId,
                    OperationId = accountTransactionData.OperationId,
                    AccountNumber = accountTransactionData.AccountNumber,
                    Amount = accountTransactionData.Amount,
                    TransactionDate = DateTime.Now
                };

                await _context.AccountTransactions.AddAsync(accountTransactions);
                await _context.SaveChangesAsync();
            }

            //return request status and user transactions data
            List<AccountTransactionData> transactionsData = await GetTransactionsDataAsync(accountTransactionData.UserId);

            TransactionsAndStatusData transactionsAndStatus = new TransactionsAndStatusData()
            {
                Code = response.Code,
                Status = response.Data,
                Transactions = transactionsData
            };

            return transactionsAndStatus;

        }

        //get user transactions data
        public async Task<List<AccountTransactionData>> GetTransactionsDataAsync(string userId)
        {

            List<AccountTransactionData> TransactionDataList = 
                                          await _context.AccountTransactions.Where(at => at.UserId == userId)
                                          .Join(
                                                _context.OperationTypes,
                                                at => at.OperationId,
                                                ot => ot.OperationId,
                                                (at, ot) => new AccountTransactionData
                                                {
                                                    TransactionId = at.TransactionId,
                                                    FullName = at.FullName,
                                                    EnglishFullName = at.EnglishFullName,
                                                    DateOfBirth = at.DateOfBirth,
                                                    UserId = at.UserId,
                                                    OperationId = at.OperationId,
                                                    OperationDesc = ot.OperationDesc,
                                                    AccountNumber = at.AccountNumber,
                                                    Amount = at.Amount,
                                                    TransactionDate = at.TransactionDate.HasValue ? at.TransactionDate.Value.Date : default(DateTime)
                                                })
                                           .ToListAsync();

            return TransactionDataList;
        }

        public async Task<TransactionsAndStatusData> DeleteTransactionAsync(int transactionId)
        {
            AccountTransactions? accountTransactions = await _context.AccountTransactions.Where(at => at.TransactionId == transactionId).FirstOrDefaultAsync();
            TransactionsAndStatusData transactionsAndStatus;

            if (accountTransactions != null)
            {
                // update transaction to bank
                ApiResponse response = await _bankService.DeleteBankTransactionAsync(transactionId);

                // check if success
                if (response.Code >= 200 && response.Code <= 299)
                {
                    // delete in db
                    _context.AccountTransactions.Remove(accountTransactions);
                    await _context.SaveChangesAsync();
                }

                //return request status and user transactions data
                List<AccountTransactionData> transactionsData = await GetTransactionsDataAsync(accountTransactions.UserId);

                transactionsAndStatus = new TransactionsAndStatusData()
                {
                    Code = response.Code,
                    Status = response.Data,
                    Transactions = transactionsData
                };

                return transactionsAndStatus;
            }

            return transactionsAndStatus = new TransactionsAndStatusData()
            {
                Code = 500,
                Status = "Not Found",
                Transactions = null
            };

        }

        public async Task<TransactionsAndStatusData> UpdateTransactionAsync(AccountTransactionData accountTransactionData)
        {
            AccountTransactions? accountTransactions = await _context.AccountTransactions.Where(at => at.TransactionId == accountTransactionData.TransactionId).FirstOrDefaultAsync();
            TransactionsAndStatusData transactionsAndStatus;

            if (accountTransactions != null)
            {
                // update transaction to bank
                ApiResponse response = await _bankService.UpdateBankTransactionAsync(accountTransactionData);

                // check if success
                if (response.Code >= 200 && response.Code <= 299)
                {
                    // update in db
                    accountTransactions.OperationId = accountTransactionData.OperationId;
                    accountTransactions.AccountNumber = accountTransactionData.AccountNumber;
                    accountTransactions.Amount = accountTransactionData.Amount;
                    accountTransactions.TransactionDate = DateTime.Now;

                    await _context.SaveChangesAsync();
                }

                //return request status and user transactions data
                List<AccountTransactionData> transactionsData = await GetTransactionsDataAsync(accountTransactions.UserId);

                transactionsAndStatus = new TransactionsAndStatusData()
                {
                    Code = response.Code,
                    Status = response.Data,
                    Transactions = transactionsData
                };

                return transactionsAndStatus;
            }

            return transactionsAndStatus = new TransactionsAndStatusData()
            {
                Code = 500,
                Status = "Not Found",
                Transactions = null
            };
        }

    }




}


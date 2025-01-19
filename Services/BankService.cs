using PhoenixTaskApp.Data.Context;
using System.Text.Json;
using System.Text;
using PhoenixTaskApp.Core.BusinessEntities.Global;
using Azure;
using Microsoft.AspNetCore.DataProtection;
using System.Net.Http.Headers;
using PhoenixTaskApp.Core.BusinessEntities.AccountTransaction;

namespace PhoenixTaskApp.Services
{
    public class BankService
    {
        private readonly HttpClient _httpClient;

        public BankService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<ApiResponse> GetTokenAsync(TokenRequest tokenRequest)
        {
            try
            {
                var tokenContent = new StringContent(
                    JsonSerializer.Serialize(tokenRequest),
                    Encoding.UTF8,
                    "application/json"
                );

                var response = await _httpClient.PostAsync("https://openBanking/createtoken", tokenContent); //todo: save link in appsetings.json
                var responseContent = await response.Content.ReadAsStringAsync();

                if (!response.IsSuccessStatusCode)
                {
                    return new ApiResponse
                    {
                        Code = (int)response.StatusCode,
                        Data = null
                    };
                }

                return new ApiResponse
                {
                    Code = (int)response.StatusCode,
                    Data = responseContent
                };
            }
            catch (Exception)
            {
                return new ApiResponse
                {
                    Code = 500,
                    Data = null
                };
            }

        }

        public async Task<ApiResponse> AddBankTransactionAsync(AccountTransactionData accountTransactionData)
        {

            //********************** This is a dummy reading***********************

            //try
            //{
            //    TokenRequest tokenRequest = new TokenRequest
            //    {
            //        UserId = accountTransactionData.UserId,
            //        SecretId = "Je45GDf34"  //todo: save in appsetings.json
            //    };

            //     //Get token
            //    ApiResponse response = await GetTokenAsync(tokenRequest);

            //    if (response.Code == 200)
            //    {
            //        var operationUrl = accountTransactionData.OperationId == 1
            //            ? "https://openBanking/createdeposit" //todo: save link in appsetings.json
            //            : "https://openBanking/createWithdrawal";

            //        var operationRequestData = new
            //        {
            //            amount = accountTransactionData.Amount,
            //            bank = accountTransactionData.AccountNumber
            //        };

            //        var operationContent = new StringContent(
            //            JsonSerializer.Serialize(operationRequestData),
            //            Encoding.UTF8,
            //            "application/json"
            //        );
            
            //        //Add the token to the request headers
            //        var token = response.Data;
            //        _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            //        //Send request
            //        var operationResponse = await _httpClient.PostAsync(operationUrl, operationContent);

            //        if (!operationResponse.IsSuccessStatusCode)
            //        {
            //            return new ApiResponse
            //            {
            //                Code = (int)operationResponse.StatusCode,
            //                Data = "Failed"
            //            };
            //        }

            //        return new ApiResponse
            //        {
            //            Code = (int)operationResponse.StatusCode,
            //            Data = "Success"
            //        };

            //    }
            //    else
            //    {
            //        return new ApiResponse
            //        {
            //            Code = response.Code,
            //            Data = "Failed"
            //        };
            //    }
            //}
            //catch (Exception)
            //{
            //    return new ApiResponse
            //    {
            //        Code = 500,
            //        Data = "Failed"
            //    };
            //}

            return new ApiResponse
            {
                Code = 200,
                Data = "Success"
            };
        }

        public async Task<ApiResponse> DeleteBankTransactionAsync(int transactionId)
        {
            //todo: get token and delete transaction in bank
            return new ApiResponse
            {
                Code = 200,
                Data = "Success"
            };
        }


        public async Task<ApiResponse> UpdateBankTransactionAsync(AccountTransactionData accountTransactionData)
        {
            //todo: get token and update transaction in bank
            return new ApiResponse
            {
                Code = 200,
                Data = "Success"
            };
        }



    }

}


using BankingApi.Core.Domain;
using BankingApi.Services.Responses;
using System;
using System.Collections.Generic;
using System.Text;

namespace BankingApi.Services.Contracts
{
    public interface IAccountService
    {
        BasicResponse<Account> GetAccountById(int accountId, int memberId);
    }
}

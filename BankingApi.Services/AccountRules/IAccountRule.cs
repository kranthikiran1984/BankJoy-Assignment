using BankingApi.Core.Domain;
using BankingApi.Services.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace BankingApi.Services.AccountRules
{
    public interface IAccountRule
    {
        int Order { get; }
        bool PassRule(Account account, TransactionData transactionData);
    }
}

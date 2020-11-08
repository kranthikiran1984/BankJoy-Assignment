using System;
using System.Collections.Generic;
using System.Text;
using BankingApi.Core.Domain;
using BankingApi.Services.Model;

namespace BankingApi.Services.AccountRules
{
    public class HasEnoughBalance : IAccountRule
    {
        private int _order;

        public HasEnoughBalance()
        {
            _order = 1;
        }

        public HasEnoughBalance(int order)
        {
            _order = order;
        }

        public int Order => _order;

        public bool PassRule(Account account, TransactionData transactionData)
        {
            return (account.Balance >= transactionData.Amount);
        }
    }
}

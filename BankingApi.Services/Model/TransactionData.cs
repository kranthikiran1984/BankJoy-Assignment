using BankingApi.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace BankingApi.Services.Model
{
    public class TransactionData
    {
        public int InstitutionId { get; set; }
        public int FromMemberId { get; set; }
        public int FromAccountId { get; set; }

        public int ToMemberId { get; set; }
        public int ToAccountId { get; set; }

        //The existing properties could have been re-used, but for clarity's sake new properties are created
        public int MemberId { get; set; }
        public int AccountId { get; set; }

        public decimal Amount { get; set; }
        public Transaction Command { get; set; }
    }
}

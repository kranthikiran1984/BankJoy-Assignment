using System;
using System.Collections.Generic;
using System.Text;

namespace BankingApi.Core.Domain
{
    public class Account: BaseEntity
    {
        public decimal Balance { get; set; }
    }
}

using BankingApi.Core.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace BankingApi.Services.Events
{
    public class AccountCreatedEvent
    {
        public Account Account { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime CreatedBy { get; set; }
    }
}

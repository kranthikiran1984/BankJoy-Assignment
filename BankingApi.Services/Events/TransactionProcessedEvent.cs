using BankingApi.Services.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace BankingApi.Services.Events
{
    public class TransactionProcessedEvent
    {
        public TransactionData TransactionData { get; set; }

        public DateTime ProcessedDate { get; set; }
    }
}

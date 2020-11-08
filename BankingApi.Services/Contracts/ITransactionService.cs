using BankingApi.Services.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BankingApi.Services.Contracts
{
    public interface ITransactionService
    {
        Task<bool> ProcessTransaction(TransactionData trasanctionData);
    }
}

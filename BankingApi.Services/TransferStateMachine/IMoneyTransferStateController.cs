using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BankingApi.Services.TransferStateMachine
{
    public interface IMoneyTransferStateController
    {
        Task<bool> TransferMoney();
    }
}

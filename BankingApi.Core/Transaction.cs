using System;
using System.Collections.Generic;
using System.Text;

namespace BankingApi.Core
{
    public enum Transaction
    {
        TRANSFER,
        DEPOSIT,
        WITHDRAW,
        BALANCE,
        HELP,
        FREEZE,
        CLOSE
    }
}

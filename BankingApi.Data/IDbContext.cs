using JsonFlatFileDataStore;
using System;
using System.Collections.Generic;
using System.Text;

namespace BankingApi.Data
{
    public interface IDbContext
    {
        DataStore BankingDataStore {get;}
    }
}

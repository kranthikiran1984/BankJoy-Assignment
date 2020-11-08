using JsonFlatFileDataStore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace BankingApi.Data
{
    public class BankingDataContext : IDbContext
    {
        const string _dbFileName = "database.json";
        DataStore _dataStore;

        public BankingDataContext()
        {
            InitDatabase();
        }

        public BankingDataContext(string dbFileName)
        {
            if (!File.Exists(dbFileName))
                File.Create(dbFileName);

            InitDatabase();
        }

        public DataStore BankingDataStore => _dataStore;

        #region Private Methods

        private void InitDatabase()
        {
            _dataStore = new DataStore(_dbFileName);
        }
        #endregion
    }
}

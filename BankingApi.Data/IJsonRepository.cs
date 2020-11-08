using BankingApi.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace BankingApi.Data
{
    public interface IJsonRepository<TEntity> : IRepository<TEntity> where TEntity :BaseEntity
    {
        int GetNextKey();
    }
}

using BankingApi.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankingApi.Data
{
    public interface IRepository<TEntity> where TEntity :BaseEntity
    {
        TEntity GetById(int id);

        Task Insert(TEntity entity);

        Task Insert(IEnumerable<TEntity> entities);

        Task Update(TEntity entity);

        Task Update(IEnumerable<TEntity> entities);

        Task Delete(int id);

        Task Delete(IEnumerable<int> id);

        IEnumerable<TEntity> GetAll();
    }
}

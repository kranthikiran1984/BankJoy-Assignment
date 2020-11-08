using BankingApi.Core;
using JsonFlatFileDataStore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankingApi.Data
{
    public class JsonRepository<TEntity> : IJsonRepository<TEntity> where TEntity : BaseEntity
    {
        private readonly IDbContext _context;

        private IEnumerable<TEntity> _entities;

        public async Task Delete(int entityId)
        {
            await (GetAll() as IDocumentCollection<TEntity>).DeleteOneAsync(entityId);
        }

        public async Task Delete(IEnumerable<int> entityIds)
        {
            if (entityIds != null)
            {
                var taskCollection = new List<Task>();

                foreach (var entityId in entityIds)
                    taskCollection.Add((GetAll() as IDocumentCollection<TEntity>).DeleteOneAsync(entityId));

                await Task.WhenAll(taskCollection.ToArray());
            }
        }

        public IEnumerable<TEntity> GetAll()
        {
            return _context.BankingDataStore.GetCollection<TEntity>().AsQueryable();
        }

        public TEntity GetById(int id)
        {
            return _context.BankingDataStore.GetItem<TEntity>(id.ToString());
        }

        public int GetNextKey()
        {
            return _context.BankingDataStore.GetCollection<TEntity>().GetNextIdValue();
        }

        public async Task Insert(TEntity entity)
        {
            await _context.BankingDataStore.InsertItemAsync<TEntity>(entity.Id.ToString(), entity);
        }

        public async Task Insert(IEnumerable<TEntity> entities)
        {
            if(entities != null)
            {
                var taskCollection = new List<Task>();

                foreach (var entity in entities)
                    taskCollection.Add(_context.BankingDataStore.InsertItemAsync<TEntity>(entity.Id.ToString(), entity));

                await Task.WhenAll(taskCollection.ToArray());
            }
        }

        public async Task Update(TEntity entity)
        {
            await (GetAll() as IDocumentCollection<TEntity>).UpdateOneAsync(entity.Id, entity);
        }

        public async Task Update(IEnumerable<TEntity> entities)
        {
            if (entities != null)
            {
                var taskCollection = new List<Task>();

                foreach (var entity in entities)
                    taskCollection.Add((GetAll() as IDocumentCollection<TEntity>).UpdateOneAsync(entity.Id, entity));

                await Task.WhenAll(taskCollection.ToArray());
            }
        }
    }
}

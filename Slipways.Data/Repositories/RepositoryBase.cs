using com.b_velop.Slipways.Data.Contracts;
using com.b_velop.Slipways.Data.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace com.b_velop.Slipways.Data.Repositories
{
    public abstract class RepositoryBase<T> : IRepositoryBase<T> where T : class, IEntity
    {
        protected SlipwaysContext Db;
        protected ILogger<RepositoryBase<T>> _logger;
        protected string Key { get; set; }
        protected IDistributedCache DCache { get; }

        protected RepositoryBase(
            SlipwaysContext db,
            IDistributedCache cache,
            ILogger<RepositoryBase<T>> logger)
        {
            DCache = cache;
            Db = db;
            _logger = logger;
        }

        public virtual async Task<IEnumerable<T>> SelectAllAsync()
        {
            var bytes = await DCache.GetAsync(Key);
            var entities = bytes.ToObject<IEnumerable<T>>();
            return entities;
        }

        public virtual async Task<T> SelectByIdAsync(
            Guid id)
        {
            var entities = await SelectAllAsync();
            var value = entities.FirstOrDefault(_ => _.Id == id);
            return value;
        }

        public virtual async Task<IEnumerable<T>> SelectByConditionAsync(Func<T, bool> expression)
        {
            var entities = await SelectAllAsync();
            var value = entities.Where(expression);
            return value;
        }

        public virtual async Task<T> InsertAsync(
            T entity)
        {
            entity.Created = DateTime.Now;
            var result = await Db.Set<T>().AddAsync(entity);
            _ = Db.SaveChanges();
            var entities = await SelectAllAsync();
            var list = entities.ToList();
            list.Add(result.Entity);
            await DCache.RemoveAsync(Key);
            await DCache.SetAsync(Key, list.ToByteArray());
            return result.Entity;
        }

        public virtual async Task<int> InsertRangeAsync(
            IEnumerable<T> entity)
        {
            foreach (var et in entity)
            {
                et.Created = DateTime.Now;
            }
            await Db.Set<T>().AddRangeAsync(entity);
            var count = Db.SaveChanges();
            var entities = await SelectAllAsync();
            var list = entities.ToList();
            list.AddRange(entity);
            await DCache.RemoveAsync(Key);
            await DCache.SetAsync(Key, list.ToByteArray());
            return count;
        }

        public virtual async Task<int> UpdateRangeAsync(
            IEnumerable<T> entities)
        {
            int cnt = 0;
            foreach (var entity in entities)
            {
                entity.Updated = DateTime.Now;
                var dbResult = Db.Set<T>().Update(entity);
                if (dbResult != null)
                    cnt++;
            }
            _ = Db.SaveChanges();
            var dbList = await Db.Set<T>().ToListAsync();
            await DCache.RemoveAsync(Key);
            await DCache.SetAsync(Key, dbList.ToByteArray());
            return cnt;
        }

        public virtual async Task<T> UpdateAsync(
            T entity)
        {
            entity.Updated = DateTime.Now;
            var result = Db.Set<T>().Update(entity);
            _ = Db.SaveChanges();
            var dbList = await Db.Set<T>().ToListAsync();
            await DCache.RemoveAsync(Key);
            await DCache.SetAsync(Key, dbList.ToByteArray());
            return result.Entity;
        }

        public virtual async Task<T> DeleteAsync(
            Guid id)
        {
            var tmp = await Db.Set<T>().FirstOrDefaultAsync(_ => _.Id == id);
            if (tmp == null)
            {
                _logger.LogWarning(5555, $"Can't delete Entity with ID '{id}'. Entity not exsists");
                return null;
            }
            var result = Db.Set<T>().Remove(tmp);
            _ = Db.SaveChanges();
            var entities = await SelectAllAsync();
            var list = entities.ToList();
            var ne = list.Where(_ => _.Id != id);
            await DCache.RemoveAsync(Key);
            await DCache.SetAsync(Key, ne.ToByteArray());
            return result.Entity;
        }
    }
}

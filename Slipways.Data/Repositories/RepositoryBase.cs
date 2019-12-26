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
        private SlipwaysContext Db;
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
            if (id == Guid.Empty)
                throw new ArgumentNullException();
            T result = default;
            try
            {
                var tmp = await Db.Set<T>().FirstOrDefaultAsync(_ => _.Id == id);
                if (tmp == null)
                {
                    _logger.LogWarning(5555, $"Can't delete Entity with ID '{id}'. Entity not exsists");
                    return null;
                }
                result = Db.Set<T>().Remove(tmp).Entity;
                _ = Db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException e)
            {
                _logger.LogError(2323, $"Error occurred while remove Entity with ID '{id}' from Database", e);
            }
            catch (DbUpdateException e)
            {
                _logger.LogError(2424, $"Error occurred while remove Entity with ID '{id}' from Database", e);
            }
            catch (Exception e)
            {
                _logger.LogError(6666, $"Error occurred while remove Entity with ID '{id}' from Database", e);
            }
            try
            {
                var entities = await SelectAllAsync();
                var list = entities.ToList();
                var ne = list.Where(_ => _.Id == id);
                await DCache.RemoveAsync(Key);
                await DCache.SetAsync(Key, ne.ToByteArray());
                return result;
            }
            catch (ArgumentNullException e)
            {
                _logger.LogError(5656, $"Error occurred while remove Entity with ID '{id}' from Cache", e);
            }
            catch (Exception e)
            {
                _logger.LogError(6666, $"Error occurred while remove Entity with ID '{id}' from Cache", e);
            }
            return null;
        }
    }
}

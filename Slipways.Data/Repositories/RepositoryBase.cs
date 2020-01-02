using com.b_velop.Slipways.Data.Contracts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace com.b_velop.Slipways.Data.Repositories
{
    public abstract class RepositoryBase<T> : IRepositoryBase<T> where T : class, IEntity
    {
        protected SlipwaysContext Context;
        protected IMemoryCache _memoryCache;
        protected ILogger<RepositoryBase<T>> _logger;

        protected string Key { get; set; }

        protected RepositoryBase(
            SlipwaysContext context,
            IMemoryCache memoryCache,
            ILogger<RepositoryBase<T>> logger)
        {
            Context = context;
            _memoryCache = memoryCache;
            _logger = logger;
        }

        public virtual async Task<IEnumerable<T>> SelectAllAsync(
            CancellationToken cancellationToken = default)
        {
            try
            {
                if (!_memoryCache.TryGetValue(Key, out HashSet<T> values))
                {
                    var fetch = await Context.Set<T>().ToListAsync(cancellationToken);
                    values = fetch.ToHashSet();
                    _memoryCache.Set(Key, values);
                }
                return values;
            }
            catch (Exception e)
            {
                _logger.LogError(6666, $"Unexpected error occurred while selecting values", e);
            }
            return default;
        }

        public virtual async Task<T> SelectByIdAsync(
            Guid id,
            CancellationToken cancellationToken = default)
        {
            if (id == Guid.Empty)
                throw new ArgumentNullException("Id is null");

            try
            {
                var entities = await SelectAllAsync(cancellationToken);
                var value = entities.FirstOrDefault(_ => _.Id == id);
                return value;
            }
            catch (ArgumentNullException e)
            {
                _logger.LogError(6665, $"Error occurred while selecting value by ID '{id}'", e);
            }
            catch (Exception e)
            {
                _logger.LogError(6666, $"Unexpected error occurred while selecting value by ID '{id}'", e);
            }
            return default;
        }

        public virtual async Task<IEnumerable<T>> SelectByConditionAsync(
            Func<T, bool> expression,
            CancellationToken cancellationToken = default)
        {
            if (expression == null)
                throw new ArgumentNullException();

            try
            {
                var entities = await SelectAllAsync(cancellationToken);
                var value = entities.Where(expression);
                return value;
            }
            catch (ArgumentNullException e)
            {
                _logger.LogError(6665, $"Error occurred while selecting value by condition", e);
            }
            catch (Exception e)
            {
                _logger.LogError(6666, $"Unexpected error occurred while selecting value by condition", e);
            }
            return default;
        }

        /// <summary>
        /// Insert a new Entity into Context and update MemoryCache. Id and Created properties are set here
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException">Throwed when entity is null</exception>
        public virtual async Task<T> InsertAsync(
            T entity,
            CancellationToken cancellationToken = default)
        {
            if (entity == null)
                throw new ArgumentNullException("Entity was null");
            try
            {
                entity.Created = DateTime.Now;
                entity.Id = Guid.NewGuid();

                var result = await Context.Set<T>().AddAsync(entity, cancellationToken);
                _ = Context.SaveChanges();

                var entities = await SelectAllAsync(cancellationToken);
                var list = entities.ToHashSet();
                list.Add(result.Entity);
                _memoryCache.Set(Key, list);
                return result.Entity;
            }
            catch (DbUpdateConcurrencyException e)
            {
                _logger.LogError(6664, $"Error occurred while inserting new entity to database", e);
            }
            catch (DbUpdateException e)
            {
                _logger.LogError(6665, $"Error occurred while inserting new entity to database", e);
            }
            catch (Exception e)
            {
                _logger.LogError(6666, $"Unexpected error occurred while inserting new entity to database", e);
            }
            return default;
        }

        public virtual async Task<int> InsertRangeAsync(
            IEnumerable<T> entities,
            CancellationToken cancellationToken = default)
        {
            if (entities == null)
                throw new ArgumentNullException("Entities were null");

            try
            {
                foreach (var entity in entities)
                    entity.Created = DateTime.Now;

                await Context.Set<T>().AddRangeAsync(entities, cancellationToken);
                var count = Context.SaveChanges();
                var allEntities = await SelectAllAsync(cancellationToken);
                var list = allEntities.ToList();
                list.AddRange(allEntities);
                _memoryCache.Set(Key, list.ToHashSet());
                return count;
            }
            catch (ArgumentNullException e)
            {
                _logger.LogError(6663, $"Error occurred while insert values range", e);
            }
            catch (DbUpdateConcurrencyException e)
            {
                _logger.LogError(6664, $"Error occurred while inserting new values range to database", e);
            }
            catch (DbUpdateException e)
            {
                _logger.LogError(6665, $"Error occurred while inserting new values range to database", e);
            }
            catch (Exception e)
            {
                _logger.LogError(6666, $"Unexpected error occurred while insert values range", e);
            }
            return default;
        }

        public virtual async Task<int> UpdateRangeAsync(
            IEnumerable<T> entities,
            CancellationToken cancellationToken = default)
        {
            if (entities == null)
                throw new ArgumentNullException("The entities were null");

            try
            {
                int cnt = 0;
                foreach (var entity in entities)
                {
                    entity.Updated = DateTime.Now;
                    var dbResult = Context.Set<T>().Update(entity);
                    if (dbResult != null)
                        cnt++;
                }
                _ = Context.SaveChanges();
                var contextResult = await Context.Set<T>().ToListAsync(cancellationToken);
                _memoryCache.Set(Key, contextResult.ToHashSet());
                return cnt;
            }
            catch (DbUpdateConcurrencyException e)
            {
                _logger.LogError(6664, $"Error occurred while updating values range in database", e);
            }
            catch (DbUpdateException e)
            {
                _logger.LogError(6665, $"Error occurred while updating values range in database", e);
            }
            catch (Exception e)
            {
                _logger.LogError(6666, $"Unexpected error occurred while updating values range", e);
            }
            return default;
        }

        public virtual async Task<T> UpdateAsync(
            T entity,
            CancellationToken cancellationToken = default)
        {
            if (entity == null)
                throw new ArgumentNullException("Entity is null");

            try
            {
                entity.Updated = DateTime.Now;
                var result = Context.Set<T>().Update(entity);
                _ = Context.SaveChanges();
                var dbList = await Context.Set<T>().ToListAsync(cancellationToken);
                _memoryCache.Set(Key, dbList.ToHashSet());
                return result.Entity;
            }
            catch (DbUpdateConcurrencyException e)
            {
                _logger.LogError(6664, $"Error occurred while updating value in database", e);
            }
            catch (DbUpdateException e)
            {
                _logger.LogError(6665, $"Error occurred while updating value in database", e);
            }
            catch (Exception e)
            {
                _logger.LogError(6666, $"Unexpected error occurred while updating value", e);
            }
            return default;
        }

        public virtual async Task<T> DeleteAsync(
            Guid id,
            CancellationToken cancellationToken = default)
        {
            if (id == Guid.Empty)
                throw new ArgumentNullException("Id is null");

            T result = default;
            try
            {
                var tmp = await Context.Set<T>().FirstOrDefaultAsync(_ => _.Id == id, cancellationToken);
                if (tmp == null)
                {
                    _logger.LogWarning(5000, $"Can't delete Entity with ID '{id}' - Entity not exsists");
                    return null;
                }
                result = Context.Set<T>().Remove(tmp).Entity;
                _ = Context.SaveChanges();
            }
            catch (DbUpdateConcurrencyException e)
            {
                _logger.LogError(6664, $"Error occurred while remove Entity with ID '{id}' from Database", e);
            }
            catch (DbUpdateException e)
            {
                _logger.LogError(6665, $"Error occurred while remove Entity with ID '{id}' from Database", e);
            }
            catch (Exception e)
            {
                _logger.LogError(6666, $"Error occurred while remove Entity with ID '{id}' from Database", e);
            }
            try
            {
                var entities = await SelectAllAsync(cancellationToken);
                var list = entities.ToHashSet();
                list.RemoveWhere(_ => _.Id == id);
                _memoryCache.Set(Key, list);
                return result;
            }
            catch (ArgumentNullException e)
            {
                _logger.LogError(6665, $"Error occurred while remove Entity with ID '{id}' from Cache", e);
            }
            catch (Exception e)
            {
                _logger.LogError(6666, $"Unexpected error occurred while remove Entity with ID '{id}' from Cache", e);
            }
            return null;
        }
    }
}

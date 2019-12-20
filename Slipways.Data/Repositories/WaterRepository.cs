using com.b_velop.Slipways.Data.Contracts;
using com.b_velop.Slipways.Data.Helper;
using com.b_velop.Slipways.Data.Models;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace com.b_velop.Slipways.Data.Repositories
{
    public class WaterRepository : CachedRepositoryBase<Water>, IWaterRepository
    {
        public WaterRepository(
             SlipwaysContext db,
             IMemoryCache cache,
             ILogger<RepositoryBase<Water>> logger) : base(db, cache, logger)
        {
            Key = Cache.Waters;
        }

        public override async Task<IEnumerable<Water>> SelectAllAsync()
        {
            if (!_cache.TryGetValue(Cache.Waters, out IEnumerable<Water> waters))
            {
                waters = await base.SelectAllAsync();
                _cache.Set(Cache.Waters, waters);
            }
            return waters;
        }

        public async Task<IDictionary<Guid, Water>> GetWatersByIdAsync(
            IEnumerable<Guid> waterIds,
            CancellationToken cancellationToken)
        {
            var waters = await SelectAllAsync();
            var result = new Dictionary<Guid, Water>();
            foreach (var water in waters)
                if (waterIds.Contains(water.Id))
                    result[water.Id] = water;
            return result;
        }
    }
}

using com.b_velop.Slipways.Data.Contracts;
using com.b_velop.Slipways.Data.Extensions;
using com.b_velop.Slipways.Data.Helper;
using com.b_velop.Slipways.Data.Models;
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
    public class ExtraRepository : RepositoryBase<Extra>, IExtraRepository
    {
        public ExtraRepository(
            SlipwaysContext db,
            IDistributedCache dcache,
            IMemoryCache memoryCache,
            ILogger<RepositoryBase<Extra>> logger) : base(db, memoryCache, dcache, logger)
        {
            Key = Cache.Extras;
        }

        public async Task<ILookup<Guid, Extra>> GetExtrasBySlipwayIdAsync(
            IEnumerable<Guid> slipwaysIds,
            CancellationToken cancellationToken)
        {
            var extras = await SelectAllAsync();
            if(!_cache.TryGetValue(Cache.SlipwayExtras, out HashSet<SlipwayExtra> slipwayExtrasAll))
            {
                slipwayExtrasAll = Db.SlipwayExtras.ToHashSet();
                _cache.Set(Cache.SlipwayExtras, slipwayExtrasAll);
            }
            //var slipwayExtrasBytes = await DCache.GetAsync(Cache.SlipwayExtras);
            //var slipwayExtrasAll = slipwayExtrasBytes.ToObject<IEnumerable<SlipwayExtra>>();
            var extraIds = slipwayExtrasAll.Where(_ => slipwaysIds.Contains(_.SlipwayFk));
            var result = new List<Extra>();

            foreach (var extraId in extraIds)
            {
                var extra = extras.FirstOrDefault(_ => _.Id == extraId.ExtraFk);
                if (extra != null)
                    result.Add(new Extra
                    {
                        Id = extra.Id,
                        Name = extra.Name,
                        Created = extra.Created,
                        Updated = extra.Updated,
                        SlipwayFk = extraId.SlipwayFk
                    });
            }
            return result.ToLookup(_ => _.SlipwayFk);
        }
    }
}

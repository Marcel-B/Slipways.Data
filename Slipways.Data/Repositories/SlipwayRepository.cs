using com.b_velop.Slipways.Data.Contracts;
using com.b_velop.Slipways.Data.Extensions;
using com.b_velop.Slipways.Data.Helper;
using com.b_velop.Slipways.Data.Models;
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
    public class SlipwayRepository : RepositoryBase<Slipway>, ISlipwayRepository
    {
        private IExtraRepository _extraRepository;

        public SlipwayRepository(
            SlipwaysContext db,
            IDistributedCache cache,
            IMemoryCache memoryCache,
            IExtraRepository extraRepository,
            ILogger<RepositoryBase<Slipway>> logger) : base(db, memoryCache, cache, logger)
        {
            _extraRepository = extraRepository;
            Key = Cache.Slipways;
        }

        public async Task<ILookup<Guid, Slipway>> GetSlipwayByWaterIdAsync(
         IEnumerable<Guid> waterIds,
         CancellationToken cancellationToken)
        {
            var slipways = await SelectAllAsync();
            var result = new List<Slipway>();
            foreach (var slipway in slipways)
                if (waterIds.Contains(slipway.WaterFk))
                    result.Add(slipway);
            return result.ToLookup(x => x.WaterFk);
        }

        public async Task<ILookup<Guid, Slipway>> GetSlipwaysByExtraIdAsync(
             IEnumerable<Guid> extraIds,
             CancellationToken cancellationToken)
        {
            var slipways = await SelectAllAsync();
            if(!_cache.TryGetValue(Cache.SlipwayExtras, out HashSet<SlipwayExtra> slipwayExtrasAll))
            {
                slipwayExtrasAll = Db.SlipwayExtras.ToHashSet();
                _cache.Set(Cache.SlipwayExtras, slipwayExtrasAll);
            }
            //var slipwayExtrasByte = await DCache.GetAsync(Cache.SlipwayExtras);
            //var slipwayExtrasAll = slipwayExtrasByte.ToObject<IEnumerable<SlipwayExtra>>();
            var slipwayExtras = slipwayExtrasAll.Where(_ => extraIds.Contains(_.ExtraFk));

            var result = new List<Slipway>();

            foreach (var slipwayExtra in slipwayExtras)
            {
                var slipway = slipways.FirstOrDefault(_ => _.Id == slipwayExtra.SlipwayFk);
                if (slipway != null)
                    result.Add(new Slipway
                    {
                        Id = slipway.Id,
                        ExtraFk = slipwayExtra.ExtraFk,
                        City = slipway.City,
                        Latitude = slipway.Latitude,
                        Longitude = slipway.Longitude,
                        Name = slipway.Name,
                        Postalcode = slipway.Postalcode,
                        WaterFk = slipway.WaterFk,
                        Updated = slipway.Updated,
                        Street = slipway.Street,
                        Rating = slipway.Rating,
                        Pro = slipway.Pro,
                        Created = slipway.Created,
                        Costs = slipway.Costs,
                        Contra = slipway.Contra,
                        Comment = slipway.Comment
                    });
            }
            return result.ToLookup(x => x.ExtraFk);
        }
    }
}

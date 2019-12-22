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
            ILogger<RepositoryBase<Extra>> logger) : base(db, dcache, logger)
        {
            Key = Cache.Extras;
        }

        //public override async Task<IEnumerable<Extra>> SelectAllAsync()
        //{
        //    var extraBytes = await _dcache.GetAsync(Cache.Extras);
        //    var extras = extraBytes.ToObject<IEnumerable<Extra>>();
        //    return extras;
        //    //if (!_cache.TryGetValue(Cache.Extras, out IEnumerable<Extra> extras))
        //    //{
        //    //    extras = await base.SelectAllAsync();
        //    //    _cache.Set(Cache.Extras, extras);
        //    //}
        //    //return extras;
        //}

        public async Task<ILookup<Guid, Extra>> GetExtrasBySlipwayIdAsync(
            IEnumerable<Guid> slipwaysIds,
            CancellationToken cancellationToken)
        {
            var extras = await SelectAllAsync();
            var extraIds = Db.SlipwayExtras.Where(_ => slipwaysIds.Contains(_.SlipwayFk));
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

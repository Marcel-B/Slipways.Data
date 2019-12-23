using com.b_velop.Slipways.Data.Contracts;
using com.b_velop.Slipways.Data.Extensions;
using com.b_velop.Slipways.Data.Helper;
using com.b_velop.Slipways.Data.Models;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace com.b_velop.Slipways.Data.Repositories
{
    public class PortRepository : RepositoryBase<Port>, IPortRepository
    {
        public PortRepository(
            SlipwaysContext db,
            IDistributedCache cache,
            ILogger<RepositoryBase<Port>> logger) :
            base(db, cache, logger)
        {
            Key = Cache.Waters;
        }

        public async Task<ILookup<Guid, Port>> GetPortsByWaterIdAsync(
            IEnumerable<Guid> waterIds,
            CancellationToken cancellationToken)
        {
            var ports = await SelectAllAsync();
            var watersBytes = await DCache.GetAsync(Cache.Waters);
            var watersAll = watersBytes.ToObject<IEnumerable<Water>>();
            var waters = watersAll.Where(_ => waterIds.Contains(_.Id));
            var result = new List<Port>();
            foreach (var water in waters)
            {
                var port = ports.FirstOrDefault(_ => _.WaterFk == water.Id);
                if (port != null)
                    result.Add(port);
            }
            return result.ToLookup(_ => _.WaterFk);
        }
    }
}

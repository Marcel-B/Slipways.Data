using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using com.b_velop.Slipways.Data.Contracts;
using com.b_velop.Slipways.Data.Helper;
using com.b_velop.Slipways.Data.Models;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;

namespace com.b_velop.Slipways.Data.Repositories
{
    public class WaterRepository : RepositoryBase<Water>, IWaterRepository
    {
        public WaterRepository(
             SlipwaysContext db,
            IMemoryCache memoryCache,
             ILogger<RepositoryBase<Water>> logger) : base(db, memoryCache, logger)
        {
            Key = Cache.Waters;
        }

        public async Task<IDictionary<Guid, Water>> GetWatersByIdAsync(
            IEnumerable<Guid> waterIds,
             CancellationToken cancellationToken)
        {
            var waters = await SelectAllAsync();
            var result = waters.Where(_ => waterIds.Contains(_.Id));
            return result.ToDictionary(x => x.Id);
        }
    }
}

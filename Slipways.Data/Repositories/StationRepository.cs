using com.b_velop.Slipways.Data.Contracts;
using com.b_velop.Slipways.Data.Helper;
using com.b_velop.Slipways.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace com.b_velop.Slipways.Data.Repositories
{
    public class StationRepository : RepositoryBase<Station>, IStationRepository
    {
        public StationRepository(
            SlipwaysContext db,
            IMemoryCache memoryCache,
            ILogger<RepositoryBase<Station>> logger) : base(db, memoryCache, logger)
        {
            Key = Cache.Stations;
        }

        public async Task<ILookup<Guid, Station>> GetStationsByWaterIdAsync(
             IEnumerable<Guid> waterIds,
             CancellationToken cancellationToken)
        {
            var stations = await SelectAllAsync();
            var result = stations.Where(_ => waterIds.Contains(_.WaterFk));
            return result.ToLookup(x => x.WaterFk);
        }
    }
}

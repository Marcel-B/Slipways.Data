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
    public class WaterRepository : RepositoryBase<Water>, IWaterRepository
    {
        public WaterRepository(
             SlipwaysContext db,
             IDistributedCache dcache,
             ILogger<RepositoryBase<Water>> logger) : base(db, dcache, logger)
        {
            Key = Cache.Waters;
        }

        //public async Task<IDictionary<Guid, Water>> GetWatersByIdAsync(
        //    IEnumerable<Guid> waterIds,
        //    CancellationToken cancellationToken)
        //{
        //    var waters = await SelectAllAsync();
        //    var result = new Dictionary<Guid, Water>();
        //    foreach (var water in waters)
        //        if (waterIds.Contains(water.Id))
        //            result[water.Id] = water;
        //    return result;
        //}
    }
}

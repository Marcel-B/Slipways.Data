﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using com.b_velop.Slipways.Data.Contracts;
using com.b_velop.Slipways.Data.Helper;
using com.b_velop.Slipways.Data.Models;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Logging;

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

        public async Task<ILookup<Guid, Water>> GetWatersByIdAsync(
            IEnumerable<Guid> waterIds)
        {
            var waters = await SelectAllAsync();
            var result = waters.Where(_ => waterIds.Contains(_.Id));
            return result.ToLookup(x => x.Id);
        }
    }
}

using com.b_velop.Slipways.Data.Contracts;
using com.b_velop.Slipways.Data.Helper;
using com.b_velop.Slipways.Data.Models;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;

namespace com.b_velop.Slipways.Data.Repositories
{
    public class SlipwayExtraRepository : RepositoryBase<SlipwayExtra>, ISlipwayExtraRepository
    {
        public SlipwayExtraRepository(
            SlipwaysContext db,
            IDistributedCache cache,
            IMemoryCache memoryCache,
            ILogger<RepositoryBase<SlipwayExtra>> logger) : base(db, memoryCache, cache, logger)
        {
            Key = Cache.SlipwayExtras;
        }
    }
}

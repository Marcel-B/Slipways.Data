using com.b_velop.Slipways.Data.Contracts;
using com.b_velop.Slipways.Data.Helper;
using com.b_velop.Slipways.Data.Models;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;

namespace com.b_velop.Slipways.Data.Repositories
{
    public class SlipwayExtraRepository : RepositoryBase<SlipwayExtra>, ISlipwayExtraRepository
    {
        public SlipwayExtraRepository(
            SlipwaysContext db,
            IMemoryCache memoryCache,
            ILogger<RepositoryBase<SlipwayExtra>> logger) : base(db, memoryCache, logger)
        {
            Key = Cache.SlipwayExtras;
        }
    }
}

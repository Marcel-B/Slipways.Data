using com.b_velop.Slipways.Data.Models;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace com.b_velop.Slipways.Data.Contracts
{
    public interface IWaterRepository : ICachedRepositoryBase<Water>
    {
        Task<IDictionary<Guid, Water>> GetWatersByIdAsync(IEnumerable<Guid> waterIds, CancellationToken cancellationToken);
    }
}
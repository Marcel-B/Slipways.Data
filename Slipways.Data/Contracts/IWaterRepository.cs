using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using com.b_velop.Slipways.Data.Models;

namespace com.b_velop.Slipways.Data.Contracts
{
    public interface IWaterRepository : IRepositoryBase<Water>
    {
        Task<ILookup<Guid, Water>> GetWatersByIdAsync(IEnumerable<Guid> waterIds);
    }
}
using com.b_velop.Slipways.Data.Contracts;
using com.b_velop.Slipways.Data.Models;
using Microsoft.Extensions.Logging;

namespace com.b_velop.Slipways.Data.Repositories
{
    public class ManufacturerServicesRepository : RepositoryBase<ManufacturerService>, IManufacturerServicesRepository
    {
        public ManufacturerServicesRepository(SlipwaysContext db, ILogger<RepositoryBase<ManufacturerService>> logger) : base(db, logger)
        {
        }
    }
}

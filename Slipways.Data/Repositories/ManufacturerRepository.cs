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
    public class ManufacturerRepository : RepositoryBase<Manufacturer>, IManufacturerRepository
    {
        public ManufacturerRepository(
            SlipwaysContext db,
            IDistributedCache dcache,
            ILogger<RepositoryBase<Manufacturer>> logger) : base(db, dcache, logger)
        {
            Key = Cache.Manufacturers;
        }

        public async Task<ILookup<Guid, Manufacturer>> GetManufacturerByServiceIdAsync(
            IEnumerable<Guid> serviceIds,
            CancellationToken cancellationToken)
        {
            var manufacturers = await SelectAllAsync();
            var manufacturerServicesBytes = await DCache.GetAsync(Cache.ManufacturerServices);
            var manufacturerServicesAll = manufacturerServicesBytes.ToObject<IEnumerable<ManufacturerService>>();
            var manufacturerServices = manufacturerServicesAll.Where(_ => serviceIds.Contains(_.ServiceFk));
            var result = new List<Manufacturer>();

            foreach (var manufacturerService in manufacturerServices)
            {
                var manufacturer = manufacturers.FirstOrDefault(_ => _.Id == manufacturerService.ManufacturerFk);
                if (manufacturer != null)
                    result.Add(new Manufacturer
                    {
                        Id = manufacturer.Id,
                        Name = manufacturer.Name,
                        Updated = manufacturer.Updated,
                        Created = manufacturer.Created,
                        ServiceFk = manufacturerService.ServiceFk
                    });
            }
            return result.ToLookup(_ => _.ServiceFk);
        }
    }
}

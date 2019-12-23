using com.b_velop.Slipways.Data.Contracts;
using com.b_velop.Slipways.Data.Extensions;
using com.b_velop.Slipways.Data.Helper;
using com.b_velop.Slipways.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace com.b_velop.Slipways.Data.Repositories
{
    public class ServiceRepository : RepositoryBase<Service>, IServiceRepository
    {
        private IManufacturerRepository _rep;

        public ServiceRepository(
            SlipwaysContext db,
            IDistributedCache cache,
            IManufacturerRepository rep,
            ILogger<RepositoryBase<Service>> logger) : base(db, cache, logger)
        {
            _rep = rep;
            Key = Cache.Services;
        }

        public async Task<ILookup<Guid, Service>> GetServicesByManufacturerIdAsync(
            IEnumerable<Guid> manufacturerIds,
            CancellationToken cancellationToken)
        {
            var services = await SelectAllAsync();
            var manufacturerServicesBytes = await DCache.GetAsync(Cache.ManufacturerServices);
            var manufacturerServicesAll = manufacturerServicesBytes.ToObject<IEnumerable<ManufacturerService>>();

            var manufacturerServices = manufacturerServicesAll.Where(_ => manufacturerIds.Contains(_.ManufacturerFk));

            var result = new List<Service>();

            foreach (var manufacturerService in manufacturerServices)
            {
                var service = services.FirstOrDefault(_ => _.Id == manufacturerService.ServiceFk);
                if (service != null)
                    result.Add(new Service
                    {
                        Id = service.Id,
                        Name = service.Name,
                        Street = service.Street,
                        Postalcode = service.Postalcode,
                        City = service.City,
                        Phone = service.Phone,
                        Url = service.Url,
                        Updated = service.Updated,
                        Longitude = service.Longitude,
                        Latitude = service.Latitude,
                        Email = service.Email,
                        Created = service.Created,
                        ManufacturerFk = manufacturerService.ManufacturerFk
                    });
            }
            return result.ToLookup(_ => _.ManufacturerFk);
        }

        //public async Task<IEnumerable<Service>> GetAllIncludeAsync()
        //{
        //    var services = await Db.Services.ToListAsync();

        //    foreach (var service in services)
        //    {
        //        service
        //            .Manufacturers
        //            .AddRange(
        //                (await _rep.SelectAllAsync())
        //                    .Where(_ => Db
        //                    .ManufacturerServices
        //                    .Where(_ => _.ServiceFk == service.Id)
        //                    .Select(_ => _.ManufacturerFk)
        //                    .Contains(_.Id)));
        //    }
        //    return services;
        //}
    }
}

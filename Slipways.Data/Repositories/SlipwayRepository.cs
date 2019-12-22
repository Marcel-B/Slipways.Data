﻿using com.b_velop.Slipways.Data.Contracts;
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
    public class SlipwayRepository : RepositoryBase<Slipway>, ISlipwayRepository
    {
        private IExtraRepository _extraRepository;

        public SlipwayRepository(
            SlipwaysContext db,
            IDistributedCache cache,
            IExtraRepository extraRepository,
            ILogger<RepositoryBase<Slipway>> logger) : base(db, cache, logger)
        {
            _extraRepository = extraRepository;
        }

        public async Task<ILookup<Guid, Slipway>> GetSlipwayByWaterIdAsync(
         IEnumerable<Guid> waterIds,
         CancellationToken cancellationToken)
        {
            var slipways = await SelectAllAsync();
            var result = new List<Slipway>();
            foreach (var slipway in slipways)
                if (waterIds.Contains(slipway.WaterFk))
                    result.Add(slipway);
            return result.ToLookup(x => x.WaterFk);
        }

        public async Task<ILookup<Guid, Slipway>> GetSlipwaysByExtraIdAsync(
             IEnumerable<Guid> extraIds,
             CancellationToken cancellationToken)
        {
            var slipways = await SelectAllAsync();
            var slipwayExtrasByte = await DCache.GetAsync(Cache.SlipwayExtras);
            var slipwayExtrasAll = slipwayExtrasByte.ToObject<IEnumerable<SlipwayExtra>>();
            var slipwayExtras = slipwayExtrasAll.Where(_ => extraIds.Contains(_.ExtraFk));

            var result = new List<Slipway>();

            foreach (var slipwayExtra in slipwayExtras)
            {
                var slipway = slipways.FirstOrDefault(_ => _.Id == slipwayExtra.SlipwayFk);
                if (slipway != null)
                    result.Add(new Slipway
                    {
                        Id = slipway.Id,
                        ExtraFk = slipwayExtra.ExtraFk,
                        City = slipway.City,
                        Latitude = slipway.Latitude,
                        Longitude = slipway.Longitude,
                        Name = slipway.Name,
                        Postalcode = slipway.Postalcode,
                        WaterFk = slipway.WaterFk,
                        Updated = slipway.Updated,
                        Street = slipway.Street,
                        Rating = slipway.Rating,
                        Pro = slipway.Pro,
                        Created = slipway.Created,
                        Costs = slipway.Costs,
                        Contra = slipway.Contra,
                        Comment = slipway.Comment
                    });
            }
            return result.ToLookup(x => x.ExtraFk);
        }

        //public async Task<IEnumerable<Slipway>> SelectByExtraIdAsync(
        //    Guid extraId)
        //{
        //    var slipwayExtrasBytes = await DCache.GetAsync(Cache.SlipwayExtras);
        //    var slipwayExtrasAll = slipwayExtrasBytes.ToObject<IEnumerable<SlipwayExtra>>();
        //    var extras = slipwayExtrasAll.Where(_ => _.ExtraFk == extraId).Select(_ => _.SlipwayFk);
        //    var allSlips = await SelectIncludeAllAsync();
        //    return allSlips.Where(_ => extras.Contains(extraId));
        //}

        //public async Task<IEnumerable<Slipway>> SelectIncludeAllAsync()
        //{
        //    var slipways = await Db
        //          .Slipways
        //          .Include(_ => _.Water)
        //          .ToListAsync();

        //    foreach (var slipway in slipways)
        //    {
        //        slipway.Extras.AddRange(
        //          (await _extraRepository.SelectAllAsync())
        //              .Where(_ =>
        //                  Db.SlipwayExtras
        //                      .Where(_ => _.SlipwayFk == slipway.Id)
        //                      .Select(_ => _.ExtraFk)
        //                      .Contains(_.Id)));
        //    }
        //    return slipways;
        //}

        //public async Task<Slipway> SelectByIdIncludeAsync(
        //    Guid id)
        //{
        //    var slipway = await Db.Slipways
        //      .Include(_ => _.Water)
        //      .FirstOrDefaultAsync(_ => _.Id == id);

        //    slipway.Extras.AddRange(
        //        (await _extraRepository.SelectAllAsync())
        //            .Where(_ =>
        //                Db.SlipwayExtras
        //                    .Where(_ => _.SlipwayFk == slipway.Id)
        //                    .Select(_ => _.ExtraFk)
        //                    .Contains(_.Id)));
        //    return slipway;
        //}
    }
}

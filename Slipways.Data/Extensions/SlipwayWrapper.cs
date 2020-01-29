﻿using com.b_velop.Slipways.Data.Dtos;
using com.b_velop.Slipways.Data.Models;
using System;
using System.Linq;

namespace com.b_velop.Slipways.Data.Extensions
{
    public static class SlipwayWrapper
    {
        public static SlipwayDto ToDto(
            this Slipway s)
        {
            var slipway = new SlipwayDto
            {
                Name = s.Name,
                Rating = s.Rating,
                Comment = s.Comment,
                Street = s.Street,
                Postalcode = s.Postalcode,
                City = s.City,
                Costs = s.Costs,
                Pro = s.Pro,
                Contra = s.Contra,
                Longitude = s.Longitude,
                Latitude = s.Latitude,
                Created = s.Created,
                Id = s.Id,
                Updated = s.Updated,
                Country = s.Country,
                PortFk = s.PortFk ?? Guid.Empty,
                WaterFk = s.WaterFk,
                Port = s.Port?.ToDto(),
                Water = s.Water?.ToDto(),
                Extras = s.Extras?.Select(_ => _.ToDto())
            };
            return slipway;
        }

        public static Slipway ToClass(
            this SlipwayDto s)
        {
            var slipway = new Slipway
            {
                Name = s.Name,
                WaterFk = s.WaterFk,
                Rating = s.Rating,
                Comment = s.Comment,
                Street = s.Street,
                Postalcode = s.Postalcode,
                City = s.City,
                Costs = s.Costs,
                Pro = s.Pro,
                Contra = s.Contra,
                Country = s.Country,
                Longitude = s.Longitude,
                Latitude = s.Latitude,
                Created = s.Created,
                Id = s.Id,
                Updated = s.Updated,
                Port = s.Port?.ToClass(),
                Extras = s.Extras?.Select(_ => _.ToClass()).ToList(),
            };
            return slipway;
        }

        public static Slipway Copy(
            this Slipway s)
        {
            var slipway = new Slipway
            {
                Name = s.Name,
                WaterFk = s.WaterFk,
                Rating = s.Rating,
                Comment = s.Comment,
                Street = s.Street,
                Postalcode = s.Postalcode,
                City = s.City,
                Costs = s.Costs,
                Pro = s.Pro,
                Contra = s.Contra,
                Country = s.Country,
                Longitude = s.Longitude,
                Latitude = s.Latitude,
                Created = s.Created,
                Id = s.Id,
                Updated = s.Updated,
                ExtraFk = s.ExtraFk,
                Extras = s.Extras?.Select(_ => _.Copy())?.ToList(),
                Water = s.Water?.Copy()
            };
            return slipway;
        }
    }
}

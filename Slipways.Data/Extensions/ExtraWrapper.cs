﻿using com.b_velop.Slipways.Data.Dtos;
using com.b_velop.Slipways.Data.Models;
using System.Linq;

namespace com.b_velop.Slipways.Data.Extensions
{
    public static class ExtraWrapper
    {
        public static ExtraDto ToDto(
            this Extra e)
        {
            var extra = new ExtraDto
            {
                Id = e.Id,
                Name = e.Name
            };
            return extra;
        }

        public static Extra ToClass(
            this ExtraDto e)
        {
            var extra = new Extra
            {
                Id = e.Id,
                Name = e.Name
            };
            return extra;
        }

        public static Extra Copy(
            this Extra e)
        {
            var extra = new Extra
            {
                Id = e.Id,
                Created = e.Created,
                Name = e.Name,
                SlipwayFk = e.SlipwayFk,
                Slipways = e.Slipways?.Select(_ => _.Copy())?.ToList(),
                Updated = e.Updated,
            };
            return extra;
        }
    }
}

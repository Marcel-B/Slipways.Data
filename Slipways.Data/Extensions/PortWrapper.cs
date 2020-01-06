using com.b_velop.Slipways.Data.Dtos;
using com.b_velop.Slipways.Data.Models;
using System.Linq;

namespace com.b_velop.Slipways.Data.Extensions
{
    public static class PortWrapper
    {
        public static Port ToClass(
            this PortDto p)
        {
            var port = new Port
            {
                Phone = p.Phone,
                WaterFk = p.WaterFk,
                City = p.City,
                Created = p.Created,
                Email = p.Email,
                Id = p.Id,
                Latitude = p.Latitude,
                Longitude = p.Longitude,
                Name = p.Name,
                Postalcode = p.Postalcode,
                Street = p.Street,
                Updated = p.Updated,
                Url = p.Url,
                Slipways = p.Slipways?.Select(_ => _.ToClass()),
                Water = p.Water?.ToClass()
            };
            return port;
        }

        public static PortDto ToDto(
            this Port p)
        {
            var portDto = new PortDto
            {
                Id = p.Id,
                City = p.City,
                Created = p.Created,
                Email = p.Email,
                Latitude = p.Latitude,
                Longitude = p.Longitude,
                Name = p.Name,
                Phone = p.Phone,
                Postalcode = p.Postalcode,
                Street = p.Street,
                Updated = p.Updated,
                Url = p.Url,
                Slipways = p.Slipways?.Select(_ => _.ToDto()),
                Water = p.Water?.ToDto(),
                WaterFk = p.WaterFk,
            };
            return portDto;
        }
    }
}

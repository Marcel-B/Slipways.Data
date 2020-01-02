using com.b_velop.Slipways.Data.Dtos;
using com.b_velop.Slipways.Data.Models;
using System.Linq;

namespace com.b_velop.Slipways.Data.Extensions
{
    public static class ManufacturerWrapper
    {
        public static ManufacturerDto ToDto(
            this Manufacturer m)
        {
            var manufacturer = new ManufacturerDto
            {
                Id = m.Id,
                Name = m.Name
            };
            return manufacturer;
        }

        public static Manufacturer ToClass(
            this ManufacturerDto m)
        {
            var manufacturer = new Manufacturer
            {
                Id = m.Id,
                Name = m.Name
            };
            return manufacturer;
        }

        public static Manufacturer Copy(
            this Manufacturer m)
        {
            var manufacturer = new Manufacturer
            {
                Id = m.Id,
                Created = m.Created,
                Name = m.Name,
                ServiceFk = m.ServiceFk,
                Services = m.Services?.Select(_ => _.Copy())?.ToList(),
                Updated = m.Updated,
            };
            return manufacturer;
        }
    }
}

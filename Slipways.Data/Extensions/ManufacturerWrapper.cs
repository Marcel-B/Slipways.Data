using com.b_velop.Slipways.Data.Dtos;
using com.b_velop.Slipways.Data.Models;

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
    }
}

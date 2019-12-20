using com.b_velop.Slipways.Data.Dtos;
using com.b_velop.Slipways.Data.Models;
using System.Collections.Generic;

namespace com.b_velop.Slipways.Data.Extensions
{
    public static class ServiceWrapper
    {
        public static Service ToClass(
            this ServiceDto s)
        {
            var service = new Service
            {
                Id = s.Id,
                Name = s.Name,
                Street = s.Street,
                Postalcode = s.Postalcode,
                City = s.City,
                Longitude = s.Longitude,
                Latitude = s.Latitude,
                Url = s.Url,
                Phone = s.Phone,
                Email = s.Email,
                Manufacturers = new List<Manufacturer>(),
            };
            if (s.Manufacturers == null)
                return service;

            foreach (var manufacturer in s.Manufacturers)
                service.Manufacturers.Add(manufacturer.ToClass());
            return service;
        }

        public static ServiceDto ToDto(
            this Service s)
        {
            var service = new ServiceDto
            {
                Id = s.Id,
                Name = s.Name,
                Street = s.Street,
                Postalcode = s.Postalcode,
                City = s.City,
                Longitude = s.Longitude,
                Latitude = s.Latitude,
                Url = s.Url,
                Phone = s.Phone,
                Email = s.Email,
                Manufacturers = new List<ManufacturerDto>(),
            };
            if (s.Manufacturers == null)
                return service;

            foreach (var manufacturer in s.Manufacturers)
                service.Manufacturers.Add(manufacturer.ToDto());

            return service;
        }
    }
}

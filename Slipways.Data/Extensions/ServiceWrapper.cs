using com.b_velop.Slipways.Data.Dtos;
using com.b_velop.Slipways.Data.Models;
using System.Collections.Generic;
using System.Linq;

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

        public static Service Copy(
            this Service s)
        {
            var service = new Service
            {
                Id = s.Id,
                City = s.City,
                Created = s.Created,
                Email = s.Email,
                Latitude = s.Latitude,
                Longitude = s.Longitude,
                ManufacturerFk = s.ManufacturerFk,
                Manufacturers = s.Manufacturers?.Select(_ => _.Copy())?.ToList(),
                Name = s.Name,
                Phone = s.Phone,
                Postalcode = s.Postalcode,
                Street = s.Street,
                Updated = s.Updated,
                Url = s.Url
            };
            return service;
        }
    }
}

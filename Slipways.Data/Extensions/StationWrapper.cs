using com.b_velop.Slipways.Data.Dtos;
using com.b_velop.Slipways.Data.Models;

namespace com.b_velop.Slipways.Data.Extensions
{
    public static class StationWrapper
    {
        public static StationDto ToDto(
            this Station s)
        {
            var station = new StationDto
            {
                Id = s.Id,
                Agency = s.Agency,
                Km = s.Km,
                Latitude = s.Latitude,
                Longitude = s.Longitude,
                Longname = s.Longname,
                Number = s.Number,
                Shortname = s.Shortname,
                Water = s.Water?.ToDto()
            };
            return station;
        }

        public static Station ToClass(
            this StationDto s)
        {
            var station = new Station
            {
                Id = s.Id,
                Agency = s.Agency,
                Km = s.Km,
                Latitude = s.Latitude,
                Longitude = s.Longitude,
                Longname = s.Longname,
                Number = s.Number,
                Shortname = s.Shortname,
                Water = s.Water?.ToClass()
            };
            return station;
        }
    }
}

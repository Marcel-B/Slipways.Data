using System;
using System.Text.Json.Serialization;
using com.b_velop.Slipways.Data.Contracts;

namespace com.b_velop.Slipways.Data.Dtos
{
    public class StationDto : IDto
    {
        [JsonPropertyName("uuid")]
        public Guid Id { get; set; }

        [JsonPropertyName("number")]
        public string Number { get; set; }

        [JsonPropertyName("shortname")]
        public string Shortname { get; set; }

        [JsonPropertyName("longname")]
        public string Longname { get; set; }

        [JsonPropertyName("km")]
        public double Km { get; set; }

        [JsonPropertyName("agency")]
        public string Agency { get; set; }

        [JsonPropertyName("longitude")]
        public double Longitude { get; set; }

        [JsonPropertyName("latitude")]
        public double Latitude { get; set; }

        [JsonPropertyName("water")]
        public WaterDto Water { get; set; }
    }
}

using System;
using System.Text.Json.Serialization;
using com.b_velop.Slipways.Data.Contracts;

namespace com.b_velop.Slipways.Data.Dtos
{
    public class WaterDto : IDto
    {
        [JsonPropertyName("id")]
        public Guid Id { get; set; }

        [JsonPropertyName("shortname")]
        public string Shortname { get; set; }

        [JsonPropertyName("longname")]
        public string Longname { get; set; }
    }
}
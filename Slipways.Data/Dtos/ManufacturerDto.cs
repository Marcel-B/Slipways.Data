using System;
using System.Text.Json.Serialization;

namespace com.b_velop.Slipways.Data.Dtos
{
    public class ManufacturerDto
    {
        [JsonPropertyName("id")]
        public Guid Id { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }
    }
}

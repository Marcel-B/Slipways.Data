using System;
using System.Text.Json.Serialization;
using com.b_velop.Slipways.Data.Contracts;

namespace com.b_velop.Slipways.Data.Dtos
{
    public class ExtraDto : IDto
    {
        [JsonPropertyName("id")]
        public Guid Id { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }
    }
}

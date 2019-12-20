using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace com.b_velop.Slipways.Data.Dtos
{
    public class WaterDto
    {
        [JsonPropertyName("id")]
        public Guid Id { get; set; }

        [JsonPropertyName("shortname")]
        public string Shortname { get; set; }

        [JsonPropertyName("longname")]
        public string Longname { get; set; }
    }
}
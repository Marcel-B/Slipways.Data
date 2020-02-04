using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace com.b_velop.Slipways.Data.Dtos
{
    public class CurrentMeasurementDto
    {
        [JsonPropertyName("timestamp")]
        public DateTime Timestamp { get; set; }

        [JsonPropertyName("value")]
        public double Value { get; set; }

        [JsonPropertyName("trend")]
        public int Trend { get; set; }

        [JsonPropertyName("stateMnwMhw")]
        public string StateMnwMhw { get; set; }

        [JsonPropertyName("stateNswHsw")]
        public string StateNswHsw { get; set; }
    }
}

using com.b_velop.Slipways.Data.Contracts;
using System;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace com.b_velop.Slipways.Data.Models
{
    [Serializable]
    public abstract class Entity : IEntity
    {
        [Key]
        [JsonPropertyName("id")]
        public Guid Id { get; set; }
        public DateTime Created { get; set; }
        public DateTime? Updated { get; set; }
    }
}

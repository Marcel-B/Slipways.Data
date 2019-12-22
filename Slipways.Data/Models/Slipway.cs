﻿using com.b_velop.Slipways.Data.Contracts;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace com.b_velop.Slipways.Data.Models
{
    [Serializable]
    public class Slipway : Locationable, IEntity
    {
        public Slipway()
        {
            Extras = new List<Extra>();
        }

        public string Name { get; set; }

        public Guid WaterFk { get; set; }

        [ForeignKey("WaterFk")]
        public Water Water { get; set; }

        public int Rating { get; set; }

        public string Comment { get; set; }

        public string Street { get; set; }

        public string Postalcode { get; set; }

        public string City { get; set; }

        [Column(TypeName = "decimal(5, 2)")]
        public decimal Costs { get; set; }

        public string Pro { get; set; }

        public string Contra { get; set; }

        [NotMapped]
        public Guid ExtraFk { get; set; }

        [NotMapped]
        public List<Extra> Extras { get; set; }
    }
}

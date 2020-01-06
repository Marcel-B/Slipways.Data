﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace com.b_velop.Slipways.Data.Models
{
    [Serializable]
    public class Extra : Entity
    {
        public Extra()
        {
            Slipways = new List<Slipway>();
        }

        [Display(Name = "Name")]
        public string Name { get; set; }

        [NotMapped]
        public List<Slipway> Slipways { get; set; }

        [NotMapped]
        public Guid SlipwayFk { get; set; }
    }
}

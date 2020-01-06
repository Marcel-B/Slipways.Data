﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace com.b_velop.Slipways.Data.Models
{
    [Serializable]
    public class Manufacturer : Entity
    {
        public Manufacturer()
        {
            Services = new HashSet<Manufacturer>();
        }

        [Display(Name = "Name")]
        public string Name { get; set; }

        [NotMapped]
        public Guid ServiceFk { get; set; }

        [NotMapped]
        public ICollection<Manufacturer> Services { get; set; }
    }
}

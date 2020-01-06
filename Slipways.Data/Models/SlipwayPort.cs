using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace com.b_velop.Slipways.Data.Models
{
    [Serializable]
    public class SlipwayPort : Entity
    {
        public Guid SlipwayFk { get; set; }

        public Guid PortFk { get; set; }

        [ForeignKey("SlipwayFk")]
        public Slipway Slipway { get; set; }

        [ForeignKey("PortFk")]
        public Port Extra { get; set; }
    }
}

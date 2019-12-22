using com.b_velop.Slipways.Data.Dtos;
using com.b_velop.Slipways.Data.Models;

namespace com.b_velop.Slipways.Data.Extensions
{
    public static class WaterWrapper
    {
        public static WaterDto ToDto(
            this Water w)
        {
            var water = new WaterDto
            {
                Id = w.Id,
                Longname = w.Longname,
                Shortname = w.Shortname
            };
            return water;
        }

        public static Water ToClass(
            this WaterDto w)
        {
            var water = new Water
            {
                Id = w.Id,
                Longname = w.Longname,
                Shortname = w.Shortname
            };
            return water;
        }
    }
}

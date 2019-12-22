using com.b_velop.Slipways.Data.Dtos;
using com.b_velop.Slipways.Data.Models;

namespace com.b_velop.Slipways.Data.Extensions
{
    public static class ExtraWrapper
    {
        public static ExtraDto ToDto(
            this Extra e)
        {
            var extra = new ExtraDto
            {
                Id = e.Id,
                Name = e.Name
            };
            return extra;
        }

        public static Extra ToClass(
            this ExtraDto e)
        {
            var extra = new Extra
            {
                Id = e.Id,
                Name = e.Name
            };
            return extra;
        }
    }
}

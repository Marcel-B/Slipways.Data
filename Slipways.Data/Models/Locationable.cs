namespace com.b_velop.Slipways.Data.Models
{
    public abstract class Locationable : Entity
    {
        public double Latitude { get; set; }
        public double Longitude { get; set; }
    }
}

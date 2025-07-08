using CarX;
using DB.Meta;

namespace KSL.API.Extensions
{
    public class CarContext
    {
        public RaceCar RaceCar { get; set; }
        public Wheel[] Wheels { get; set; }
        public PlayerCarProfile Profile { get; set; }
        public UIDynostandContext DynoContext { get; set; }

        public bool IsLinked => RaceCar != null && !RaceCar.Equals(null);
    }
}

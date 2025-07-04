using CarX;
using DB.Meta;

namespace KSL.API.Extensions
{
    public class CarContext
    {
        public RaceCar RaceCar { get; set; }
        public BaseCar.Desc Desc => RaceCar?.GetDesc();
        public CarX.Car CarX => RaceCar?.carX;
        public PlayerCarProfile Profile { get; set; }
        public Wheel[] Wheels { get; set; }
        public UIDynostandContext DynoContext { get; set; }

        public bool IsLinked => RaceCar != null;

        public bool IsValid()
        {
            return RaceCar != null
                && CarX != null
                && Desc != null
                && Wheels != null
                && Wheels.Length == 4
                && Wheels[0].isValid;
        }
    }
}

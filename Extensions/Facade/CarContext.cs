using CarX;
using DB.Meta;

namespace KSL.API.Extensions
{
    public class CarContext
    {
        public RaceCar RaceCar { get; set; }

        public BaseCar.Desc Desc
        {
            get
            {
                if (RaceCar == null || RaceCar.Equals(null))
                    return null;

                try
                {
                    return RaceCar.GetDesc();
                }
                catch
                {
                    return null;
                }
            }
        }

        public CarX.Car CarX
        {
            get
            {
                if (RaceCar == null || RaceCar.Equals(null))
                    return null;
                return RaceCar.carX;
            }
        }

        public Wheel[] Wheels { get; set; }

        public PlayerCarProfile Profile { get; set; }

        public UIDynostandContext DynoContext { get; set; }

        public bool IsLinked => RaceCar != null && !RaceCar.Equals(null);

        public bool IsValid()
        {
            bool descValid = false;
            try
            {
                descValid = Desc != null;
            }
            catch
            {
                descValid = false;
            }

            bool wheelsValid = Wheels != null
                && Wheels.Length == 4
                && Wheels[0].isValid;

            return RaceCar != null
                && !RaceCar.Equals(null)
                && CarX != null
                && descValid
                && wheelsValid;
        }
    }
}

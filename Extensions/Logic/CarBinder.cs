using CarX;

namespace KSL.API.Extensions
{
    public class CarBinder : ICarBinder
    {
        public CarContext Bind(RaceCar car)
        {
            return new CarContext
            {
                RaceCar = car,
                Wheels = GetWheels(car)
            };
        }

        private Wheel[] GetWheels(RaceCar car)
        {
            var carX = car?.carX;
            if (carX == null) return null;

            var wheels = new Wheel[4];
            for (int i = 0; i < 4; i++)
                wheels[i] = carX.GetWheel((WheelIndex)i);

            return wheels;
        }
    }
}

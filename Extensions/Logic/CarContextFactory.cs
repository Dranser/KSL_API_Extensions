using CarX;

namespace KSL.API.Extensions
{
    public static class CarContextFactory
    {
        public static CarContext Create(RaceCar car)
        {
            return new CarContext
            {
                RaceCar = car,
                Wheels = GetWheels(car)
            };
        }

        private static Wheel[] GetWheels(RaceCar car)
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

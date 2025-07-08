using CarX;
using DB.Meta;

namespace KSL.API.Extensions
{
    public static class CarDataAccessor
    {
        public static BaseCar.Desc GetDesc(CarContext context)
        {
            if (context?.RaceCar == null || context.RaceCar.Equals(null))
                return null;

            try
            {
                return context.RaceCar.GetDesc();
            }
            catch
            {
                return null;
            }
        }

        public static CarX.Car GetCarX(CarContext context)
        {
            if (context?.RaceCar == null || context.RaceCar.Equals(null))
                return null;

            return context.RaceCar.carX;
        }
    }
}

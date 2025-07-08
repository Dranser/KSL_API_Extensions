namespace KSL.API.Extensions
{
    public static class CarValidator
    {
        public static bool IsValid(CarContext context)
        {
            if (context?.RaceCar == null || context.RaceCar.Equals(null))
                return false;

            var desc = CarDataAccessor.GetDesc(context);
            var carX = CarDataAccessor.GetCarX(context);

            bool descValid = desc != null;

            bool wheelsValid = context.Wheels != null &&
                               context.Wheels.Length == 4 &&
                               context.Wheels[0].isValid;

            return carX != null && descValid && wheelsValid;
        }
    }
}

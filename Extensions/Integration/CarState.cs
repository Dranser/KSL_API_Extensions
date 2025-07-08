using System;
using KSL.API.Extensions.UI;

namespace KSL.API.Extensions
{
    public static class CarState
    {
        public static event Action<CarContext> CarChanged;
        public static event Action<CarContext> CarUpdated;

        public static CarContext Current { get; private set; }

        public static void Set(CarContext newContext)
        {
            if (newContext?.RaceCar == null)
            {
                Clear();
                return;
            }

            var previousId = Current?.RaceCar?.carId;
            var newId = newContext.RaceCar.carId;

            Current = newContext;

            if (previousId.HasValue && previousId.Value == newId)
            {
                CarUpdated?.Invoke(newContext);
            }
            else
            {
                CarChanged?.Invoke(newContext);
            }
            
            UIContext.Invalidate();
        }

        public static void Clear()
        {
            Current = null;
            UIContext.Invalidate();
        }
    }
}

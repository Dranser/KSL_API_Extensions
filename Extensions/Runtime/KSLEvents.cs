using System;

namespace KSL.API.Extensions
{
    public static class KSLEvents
    {
        public static event Action<RaceCar> CarLoaded;
        public static event Action<UIDynostandContext> DynoEntered;
        public static event Action DynoExited;

        public static void OnCarLoaded(RaceCar car) => CarLoaded?.Invoke(car);
        public static void OnDynoEntered(UIDynostandContext ctx) => DynoEntered?.Invoke(ctx);
        public static void OnDynoExited() => DynoExited?.Invoke();
    }
}

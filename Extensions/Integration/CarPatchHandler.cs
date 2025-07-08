using HarmonyLib;
using System;
using System.Reflection;

namespace KSL.API.Extensions
{
    public class CarPatchHandler : IPatchHandler
    {
        public event Action<RaceCar> CarLoaded;

        public void Apply(Harmony harmony)
        {
            Patch(harmony, "RaceCar", "OnCarLoaded", typeof(CarPatchHandler).GetMethod(nameof(OnCarLoadedPostfix)));
        }

        private static void Patch(Harmony harmony, string typeName, string methodName, MethodInfo postfix)
        {
            var type = AccessTools.TypeByName(typeName);
            var method = AccessTools.Method(type, methodName);
            var finalizer = AccessTools.Method(typeof(PatcherHooks), "Finalizer");

            if (type == null || method == null || postfix == null || finalizer == null)
                return;

            harmony.Patch(method,
                postfix: new HarmonyMethod(postfix),
                finalizer: new HarmonyMethod(finalizer));
        }

        public static void OnCarLoadedPostfix(RaceCar __instance)
        {
            try
            {
                if (__instance == null || !IsValidCar(__instance))
                    return;

                KSLEvents.OnCarLoaded(__instance);
            }
            catch (Exception ex)
            {
                ExtLog.Error($"CarPatchHandler.OnCarLoaded: {ex}");
            }
        }

        private static bool IsValidCar(RaceCar car)
        {
            var type = car.GetType();
            var netProp = type.GetProperty("isNetworkCar");
            var podProp = type.GetProperty("isPodiumCar");

            bool isNetworkCar = netProp != null && (bool)netProp.GetValue(car);
            bool isPodiumCar = podProp != null && (bool)podProp.GetValue(car);

            return !isNetworkCar && !isPodiumCar;
        }
    }
}

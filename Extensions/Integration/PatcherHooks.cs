using System.Reflection;
using System;
using HarmonyLib;

namespace KSL.API.Extensions
{
    public static class PatcherHooks
    {
        public static event Action<RaceCar> CarLoaded;
        public static event Action<UIDynostandContext> DynoEntered;
        public static event Action DynoExited;

        private static readonly Harmony _harmony = new Harmony("VORTEX.Hooks");

        public static void Apply()
        {
            Patch("RaceCar", "OnCarLoaded", typeof(PatcherHooks).GetMethod(nameof(CarLoadedPatch)));
            Patch("UIDynostandContext", "OnActivate", typeof(PatcherHooks).GetMethod(nameof(DynoEnterPatch)));
            Patch("UIDynostandContext", "OnDeactivate", typeof(PatcherHooks).GetMethod(nameof(DynoExitPatch)));
        }

        private static void Patch(string typeName, string methodName, MethodInfo postfix)
        {
            try
            {
                var type = AccessTools.TypeByName(typeName);
                var method = AccessTools.Method(type, methodName);
                var finalizer = AccessTools.Method(typeof(PatcherHooks), nameof(Finalizer));

                if (type == null || method == null || postfix == null || finalizer == null)
                {
                    ExtLog.Error($"PatcherHooks: Cannot patch {typeName}.{methodName} — missing elements");
                    return;
                }

                _harmony.Patch(method,
                    postfix: new HarmonyMethod(postfix),
                    finalizer: new HarmonyMethod(finalizer));
            }
            catch (Exception ex)
            {
                ExtLog.Error(ex);
            }
        }

        private static Exception Finalizer(Exception __exception)
        {
            if (__exception != null)
                ExtLog.Error($"PatcherHooks Finalizer: {__exception}");
            return null;
        }

        public static void CarLoadedPatch(RaceCar __instance)
        {
            try
            {
                if (__instance == null || !IsValidCar(__instance)) return;
                CarLoaded?.Invoke(__instance);
            }
            catch (Exception ex)
            {
                ExtLog.Error($"CarLoadedPatch: {ex}");
            }
        }

        public static void DynoEnterPatch(UIDynostandContext __instance)
        {
            try
            {
                DynoEntered?.Invoke(__instance);
            }
            catch (Exception ex)
            {
                ExtLog.Error($"DynoEnterPatch: {ex}");
            }
        }

        public static void DynoExitPatch()
        {
            try
            {
                DynoExited?.Invoke();
            }
            catch (Exception ex)
            {
                ExtLog.Error($"DynoExitPatch: {ex}");
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

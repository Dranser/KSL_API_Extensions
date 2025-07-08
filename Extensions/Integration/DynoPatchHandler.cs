using HarmonyLib;
using System;
using System.Reflection;

namespace KSL.API.Extensions
{
    public class DynoPatchHandler : IPatchHandler
    {
        public void Apply(Harmony harmony)
        {
            Patch(harmony, "UIDynostandContext", "OnActivate", typeof(DynoPatchHandler).GetMethod(nameof(OnEnter)));
            Patch(harmony, "UIDynostandContext", "OnDeactivate", typeof(DynoPatchHandler).GetMethod(nameof(OnExit)));
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

        public static void OnEnter(UIDynostandContext __instance)
        {
            try
            {
                KSLEvents.OnDynoEntered(__instance);
            }
            catch (Exception ex)
            {
                ExtLog.Error($"DynoPatchHandler.OnEnter: {ex}");
            }
        }

        public static void OnExit()
        {
            try
            {
                KSLEvents.OnDynoExited();
            }
            catch (Exception ex)
            {
                ExtLog.Error($"DynoPatchHandler.OnExit: {ex}");
            }
        }
    }
}

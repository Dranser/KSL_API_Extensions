using HarmonyLib;
using System;
using System.Collections.Generic;

namespace KSL.API.Extensions
{
    public static class PatcherHooks
    {
        private static readonly Harmony _harmony = new Harmony("VORTEX.Hooks");
        private static readonly List<IPatchHandler> _handlers = new List<IPatchHandler>
        {
            new CarPatchHandler(),
            new DynoPatchHandler()
        };

        public static void Apply()
        {
            foreach (var handler in _handlers)
                handler.Apply(_harmony);
        }

        public static Exception Finalizer(Exception __exception)
        {
            if (__exception != null)
                ExtLog.Error($"Patcher Finalizer: {__exception}");
            return null;
        }
    }
}

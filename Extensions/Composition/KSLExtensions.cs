using System.Collections.Generic;
using System.Reflection;

namespace KSL.API.Extensions
{
    public static class KSLExtensions
    {
        private static readonly Dictionary<string, IModFeature> _features = new Dictionary<string, IModFeature>();

        public static void Init()
        {
            PatcherHooks.Apply();

            Register(new MainDispatcherFeature());
            Register(new CarLinkerFeature());

            Ready();
        }

        public static void Register(IModFeature feature)
        {
            if (_features.ContainsKey(feature.Id))
                return;

            _features[feature.Id] = feature;
            feature.OnInit();
        }

        public static void Ready()
        {
            foreach (var f in _features.Values)
                if (f.Enabled)
                    f.OnReady();
        }

        public static void Shutdown()
        {
            foreach (var f in _features.Values)
                if (f.Enabled)
                    f.OnShutdown();
        }

        public static bool Enable(string id)
        {
            if (_features.TryGetValue(id, out var feature) && !feature.Enabled)
            {
                SetEnabled(feature, true);
                feature.OnReady();
                return true;
            }
            return false;
        }

        public static bool Disable(string id)
        {
            if (_features.TryGetValue(id, out var feature) && feature.Enabled && !feature.IsSystem)
            {
                feature.OnShutdown();
                SetEnabled(feature, false);
                return true;
            }
            return false;
        }

        public static IEnumerable<IModFeature> List()
        {
            return _features.Values;
        }

        private static void SetEnabled(IModFeature feature, bool state)
        {
            var prop = feature.GetType().GetProperty("Enabled", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
            if (prop != null && prop.CanWrite)
                prop.SetValue(feature, state);
        }
    }
}

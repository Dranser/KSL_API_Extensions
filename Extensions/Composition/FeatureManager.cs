using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using KSL.API.Extensions.UI;

namespace KSL.API.Extensions
{
    public static class FeatureManager
    {
        private static readonly Dictionary<string, IModFeature> _features = new Dictionary<string, IModFeature>();

        public static void Init()
        {
            PatcherHooks.Apply();

            Register(new MainDispatcherFeature());
            Register(new GameStateTrackerFeature());
            Register(new CarLinkerFeature());
            Register(new UIFeature());

            Ready();
        }

        public static void Register(IModFeature feature)
        {
            if (feature == null || string.IsNullOrWhiteSpace(feature.Id))
                return;

            if (_features.ContainsKey(feature.Id))
                return;

            if (!feature.Enabled)
            {
                ExtLog.Info($"Skipping disabled feature: {feature.Id}");
                return;
            }

            _features[feature.Id] = feature;
            feature.Init();
            ExtLog.Info($"Registered: {feature.Id}");
        }

        public static void Discover(Assembly assembly)
        {
            foreach (var type in assembly.GetTypes())
            {
                if (!typeof(IModFeature).IsAssignableFrom(type))
                    continue;

                if (type.GetCustomAttribute<ModFeatureAttribute>() == null)
                    continue;

                if (type.GetCustomAttribute<ExampleAttribute>() != null)
                    continue;

                if (type.GetConstructor(Type.EmptyTypes) == null)
                {
                    ExtLog.Warning($"Skipping {type.FullName}: no parameterless constructor");
                    continue;
                }

                var feature = (IModFeature)Activator.CreateInstance(type);
                Register(feature);
            }
        }

        public static void Ready()
        {
            foreach (var f in _features.Values.Where(f => f.Enabled))
                f.Ready();
        }

        public static void Shutdown()
        {
            foreach (var f in _features.Values.Where(f => f.Enabled))
                f.Shutdown();
        }

        public static bool Enable(string id)
        {
            if (_features.TryGetValue(id, out var f) && !f.Enabled)
            {
                f.Enabled = true;
                f.Ready();
                ExtLog.Info($"Enabled: {id}");
                return true;
            }
            return false;
        }

        public static bool Disable(string id)
        {
            if (_features.TryGetValue(id, out var f) && f.Enabled)
            {
                f.Shutdown();
                f.Enabled = false;
                ExtLog.Info($"Disabled: {id}");
                return true;
            }
            return false;
        }

        public static IEnumerable<IModFeature> List() => _features.Values;
        public static T Get<T>() where T : class, IModFeature
        {
            return _features.Values.OfType<T>().FirstOrDefault();
        }
    }
}

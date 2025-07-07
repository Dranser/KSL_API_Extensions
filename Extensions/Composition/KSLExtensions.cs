using System;
using System.Collections.Generic;
using System.Reflection;
using KSL.API.Extensions.UI;

namespace KSL.API.Extensions
{
    public static class KSLExtensions
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
            if (feature == null || string.IsNullOrEmpty(feature.Id))
                return;

            if (_features.ContainsKey(feature.Id))
                return;

            if (!feature.Enabled)
            {
                ExtLog.Info($"Skipping registration of disabled feature: {feature.Id}");
                return;
            }

            _features[feature.Id] = feature;
            feature.OnInit();
            ExtLog.Info($"Registered feature: {feature.Id}");
        }

        public static void DiscoverFeatures(Assembly assembly)
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

        public static void DiscoverUI(Assembly assembly)
        {
            UIContext.DiscoverUI(assembly);
        }

        public static void Ready()
        {
            foreach (var f in _features.Values)
            {
                if (f.Enabled)
                    f.OnReady();
            }
        }

        public static void Shutdown()
        {
            foreach (var f in _features.Values)
            {
                if (f.Enabled)
                    f.OnShutdown();
            }
        }

        public static void UpdateAll()
        {
            foreach (var f in _features.Values)
            {
                if (f.Enabled && f is IModFeatureLifecycle lifecycle)
                    lifecycle.Update();
            }
        }

        public static void DrawAll()
        {
            foreach (var f in _features.Values)
            {
                if (f.Enabled && f is IModFeatureLifecycle lifecycle)
                    lifecycle.Draw();
            }
        }

        public static bool Enable(string id)
        {
            if (_features.TryGetValue(id, out var feature) && !feature.Enabled)
            {
                SetEnabled(feature, true);
                feature.OnReady();
                ExtLog.Info($"Feature enabled: {id}");
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
                ExtLog.Info($"Feature disabled: {id}");
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

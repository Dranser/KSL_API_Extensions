using System.Reflection;

namespace KSL.API.Extensions
{
    public static class KSLRuntime
    {
        private static bool _initialized = false;

        public static void Init(Assembly assembly)
        {
            if (_initialized) return;

            FeatureManager.Init();
            FeatureManager.Discover(assembly);

            _initialized = true;
        }

        public static void Update()
        {
            foreach (var feature in FeatureManager.List())
            {
                if (feature.Enabled && feature is IModFeatureLifecycle lifecycle)
                    lifecycle.Update();
            }
        }

        public static void Draw()
        {
            foreach (var feature in FeatureManager.List())
            {
                if (feature.Enabled && feature is IModFeatureLifecycle lifecycle)
                    lifecycle.Draw();
            }
        }

        public static void Shutdown()
        {
            FeatureManager.Shutdown();
        }
    }
}

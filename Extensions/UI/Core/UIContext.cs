using System;
using System.Reflection;

namespace KSL.API.Extensions.UI
{
    public static class UIContext
    {
        public static event Action InvalidateRequested;

        public static void Init(Assembly assembly)
        {
            Discover(assembly);
        }

        public static void Discover(Assembly assembly)
        {
            foreach (var type in assembly.GetTypes())
            {
                if (type.GetCustomAttribute<UIWindowAttribute>() != null &&
                    typeof(IUIWindowBuilder).IsAssignableFrom(type))
                {
                    var instance = (IUIWindowBuilder)Activator.CreateInstance(type);
                    if (instance == null) continue;

                    var window = new UIWindow(instance.Id, instance.Title, instance.Width, instance.Height);
                    window.Build(instance.Build);
                    RegisterWindow(window, instance.Condition);
                }
            }
        }

        public static void OnGUI()
        {
            UIStyle.Init();

            UIWindowRegistry.DrawAll();
            UINotificationManager.DrawAll();
        }

        public static void RegisterWindow(UIWindow window, Func<bool> condition = null)
        {
            UIWindowRegistry.Register(window, condition);
        }

        public static void ShowNotification(string message, float duration = 3f)
        {
            UINotificationManager.Show(message, duration);
        }

        public static void Invalidate()
        {
            InvalidateRequested?.Invoke();
        }
    }
}

using System;
using System.Reflection;

namespace KSL.API.Extensions.UI
{
    public static class UIContext
    {
        public static void Init()
        {
        }

        public static void DiscoverUI(Assembly assembly)
        {
            foreach (var type in assembly.GetTypes())
            {
                if (type.GetCustomAttribute<UIWindowAttribute>() != null)
                {
                    var method = type.GetMethod("Create", BindingFlags.Public | BindingFlags.Static);
                    if (method != null)
                    {
                        var window = (UIWindow)method.Invoke(null, null);

                        var conditionAttr = type.GetCustomAttribute<DrawConditionAttribute>();
                        DrawConditionType? condition = conditionAttr?.Condition;

                        RegisterWindow(window, condition);
                    }
                    else
                    {
                        ExtLog.Warning($"UI: No Create() method in {type.FullName}");
                    }
                }
            }
        }

        public static void OnGUI()
        {
            UIStyle.Init();

            UIWindowRegistry.DrawAll();
            UINotificationManager.DrawAll();
            UIHUDManager.DrawAll();
        }

        public static void RegisterWindow(UIWindow window, DrawConditionType? condition = null)
        {
            UIWindowRegistry.Register(window, condition);
        }

        public static void ShowNotification(string message, float duration = 3f)
        {
            UINotificationManager.Show(message, duration);
        }

        public static void RegisterHUD(string id, Action<UnityEngine.Rect> drawer)
        {
            UIHUDManager.Register(id, drawer);
        }
    }
}

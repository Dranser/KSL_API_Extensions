using System.Collections.Generic;

namespace KSL.API.Extensions.UI
{
    public static class UIWindowRegistry
    {
        private static readonly List<UIWindow> _windows = new List<UIWindow>();

        public static void Register(UIWindow window)
        {
            if (!_windows.Contains(window))
                _windows.Add(window);
        }

        public static void DrawAll()
        {
            foreach (var window in _windows)
                window.Draw();
        }
    }
}

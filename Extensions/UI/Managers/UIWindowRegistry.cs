using System.Collections.Generic;
using KSL.API.Extensions;

namespace KSL.API.Extensions.UI
{
    public static class UIWindowRegistry
    {
        private class WindowEntry
        {
            public UIWindow Window;
            public DrawConditionType Condition;
        }

        private static readonly List<WindowEntry> _windows = new List<WindowEntry>();

        public static void Register(UIWindow window, DrawConditionType condition = DrawConditionType.None)
        {
            if (window == null)
                return;
            if (_windows.Exists(e => e.Window == window))
                return;
            _windows.Add(new WindowEntry { Window = window, Condition = condition });
        }

        public static void DrawAll()
        {
            foreach (var entry in _windows)
            {
                if (ShouldDraw(entry.Condition))
                    entry.Window.Draw();
            }
        }

        private static bool ShouldDraw(DrawConditionType condition)
        {
            switch (condition)
            {
                case DrawConditionType.None:
                    return true;
                case DrawConditionType.CarValid:
                    return GameContext.CarIsValid;
                case DrawConditionType.OnTrack:
                    return GameContext.IsOnTrack;
                case DrawConditionType.CarReadyOnTrack:
                    return GameContext.CarIsValid && GameContext.IsOnTrack;
                default:
                    return true;
            }
        }

        public static void Clear()
        {
            _windows.Clear();
        }
    }
}

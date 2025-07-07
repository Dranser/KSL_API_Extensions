using System.Collections.Generic;

namespace KSL.API.Extensions.UI
{
    public static class UIWindowRegistry
    {
        private class Entry
        {
            public UIWindow Window;
            public DrawConditionType? Condition;
        }

        private static readonly List<Entry> _entries = new List<Entry>();

        public static void Register(UIWindow window, DrawConditionType? condition = null)
        {
            if (window == null)
                return;

            if (_entries.Exists(e => e.Window == window))
                return;

            var entry = new Entry { Window = window, Condition = condition };
            _entries.Add(entry);

            UIContext.InvalidateRequested += window.RequestRebuild;
        }

        public static void DrawAll()
        {
            foreach (var entry in _entries)
            {
                if (!IsVisible(entry.Condition))
                    continue;

                entry.Window.Draw();
            }
        }

        private static bool IsVisible(DrawConditionType? condition)
        {
            if (!condition.HasValue)
                return true;

            switch (condition.Value)
            {
                case DrawConditionType.None:
                    return true;
                case DrawConditionType.CarValid:
                    return GameContext.CarIsValid;
                case DrawConditionType.OnTrack:
                    return GameContext.IsOnTrack;
                case DrawConditionType.CarReadyOnTrack:
                    return GameContext.CarIsValid && GameContext.IsOnTrack;
                case DrawConditionType.ActiveSession:
                    return GameContext.IsInGarage || GameContext.IsOnTrack;
                default:
                    return true;
            }
        }
    }
}

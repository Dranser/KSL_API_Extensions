using System;
using System.Collections.Generic;

namespace KSL.API.Extensions.UI
{
    public static class UIWindowRegistry
    {
        private class Entry
        {
            public UIWindow Window;
            public Func<bool> Condition;
        }

        private static readonly List<Entry> _entries = new List<Entry>();

        public static void Register(UIWindow window, Func<bool> condition = null)
        {
            if (window == null) return;
            if (_entries.Exists(e => e.Window == window)) return;

            _entries.Add(new Entry
            {
                Window = window,
                Condition = condition
            });

            UIContext.InvalidateRequested += window.RequestRebuild;
        }

        public static void DrawAll()
        {
            foreach (var entry in _entries)
            {
                if (entry.Condition == null || entry.Condition())
                    entry.Window.Draw();
            }
        }
    }
}

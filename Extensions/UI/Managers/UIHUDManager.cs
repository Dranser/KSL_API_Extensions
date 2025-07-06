using System;
using System.Collections.Generic;
using UnityEngine;

namespace KSL.API.Extensions.UI
{
    public static class UIHUDManager
    {
        private class HUDItem
        {
            public string Id;
            public Action<Rect> Drawer;
            public bool Visible;
        }

        private static readonly Dictionary<string, HUDItem> _hudItems = new Dictionary<string, HUDItem>();

        public static void Register(string id, Action<Rect> drawer)
        {
            if (string.IsNullOrEmpty(id) || drawer == null)
                return;

            _hudItems[id] = new HUDItem
            {
                Id = id,
                Drawer = drawer,
                Visible = true
            };
        }

        public static void Unregister(string id)
        {
            if (!string.IsNullOrEmpty(id))
                _hudItems.Remove(id);
        }

        public static void Toggle(string id)
        {
            if (_hudItems.TryGetValue(id, out var item))
                item.Visible = !item.Visible;
        }

        public static void Show(string id)
        {
            if (_hudItems.TryGetValue(id, out var item))
                item.Visible = true;
        }

        public static void Hide(string id)
        {
            if (_hudItems.TryGetValue(id, out var item))
                item.Visible = false;
        }

        public static void DrawAll()
        {
            foreach (var item in _hudItems.Values)
            {
                if (!item.Visible)
                    continue;

                var rect = new Rect(20f, 20f, 300f, 30f);
                item.Drawer?.Invoke(rect);
            }
        }
    }
}

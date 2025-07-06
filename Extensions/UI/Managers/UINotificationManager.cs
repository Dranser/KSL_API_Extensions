using System.Collections.Generic;
using UnityEngine;

namespace KSL.API.Extensions.UI
{
    public static class UINotificationManager
    {
        private class Notification
        {
            public string Text;
            public float RemainingTime;
        }

        private static readonly List<Notification> _notifications = new List<Notification>();

        public static void Show(string message, float duration)
        {
            _notifications.Add(new Notification
            {
                Text = message,
                RemainingTime = duration
            });
        }

        public static void DrawAll()
        {
            if (_notifications.Count == 0) return;

            float y = 20f;

            GUIStyle style = UIStyle.NotificationStyle;

            for (int i = _notifications.Count - 1; i >= 0; i--)
            {
                var notif = _notifications[i];
                notif.RemainingTime -= Time.deltaTime;

                float width = Mathf.Clamp(notif.Text.Length * 7f + 40f, 200f, 400f);
                float height = style.CalcHeight(new GUIContent(notif.Text), width);

                var rect = new Rect(Screen.width * 0.5f - width * 0.5f, y, width, height + 10f);

                GUI.Box(rect, notif.Text, style);

                y += rect.height + 10f;

                if (notif.RemainingTime <= 0f)
                    _notifications.RemoveAt(i);
            }
        }

        public static void Clear()
        {
            _notifications.Clear();
        }
    }
}

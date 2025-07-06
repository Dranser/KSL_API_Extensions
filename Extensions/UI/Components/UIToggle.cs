using UnityEngine;

namespace KSL.API.Extensions.UI
{
    public class UIToggle : UIElementBase
    {
        private readonly System.Func<bool> _getter;
        private readonly System.Action<bool> _setter;
        private readonly UIMetaAttribute _meta;

        public override UIMetaAttribute Meta => _meta;

        public UIToggle(UIMetaAttribute meta, System.Func<bool> getter, System.Action<bool> setter)
        {
            _meta = meta;
            _getter = getter;
            _setter = setter;
        }

        public override float GetHeight(float width) => 28f;

        public override void Draw(Rect rect)
        {
            bool value = _getter();
            GUI.Box(rect, GUIContent.none, UIStyle.ButtonStyle);

            var labelRect = new Rect(rect.x + 8f, rect.y + 4f, rect.width - 60f, 20f);
            GUI.Label(labelRect, _meta.DisplayName, UIStyle.LabelStyle);

            var toggleRect = new Rect(rect.x + rect.width - 40f, rect.y + 4f, 32f, 20f);
            Color bgColor = value ? new Color32(0, 191, 207, 255) : new Color32(60, 60, 60, 255);
            Color knobColor = Color.white;

            GUI.DrawTexture(toggleRect, UITextureGenerator.GenerateForRect(32, 20, bgColor, 10, 10));

            float knobX = value ? toggleRect.x + 16f : toggleRect.x;
            var knobRect = new Rect(knobX, toggleRect.y, 16f, 20f);
            GUI.DrawTexture(knobRect, UITextureGenerator.GenerateForRect(16, 20, knobColor, 10, 10));

            if (GUI.Button(rect, GUIContent.none, GUIStyle.none))
            {
                bool toggledValue = !value;
                _setter(toggledValue);
                UIContext.ShowNotification(UINotificationFormatter.Format(
                    UINotificationEvent.Toggled,
                    _meta.DisplayName,
                    toggledValue ? "On" : "Off"
                ));
            }
        }
    }
}

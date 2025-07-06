using UnityEngine;

namespace KSL.API.Extensions.UI
{
    public class UISlider : UIElementBase
    {
        private readonly System.Func<float> _getter;
        private readonly System.Action<float> _setter;
        private readonly System.Action<float> _onSet;
        private readonly float _min;
        private readonly float _max;
        private readonly float _step;
        private readonly UIMetaAttribute _meta;

        private bool _isDragging = false;

        public override UIMetaAttribute Meta => _meta;

        public UISlider(UIMetaAttribute meta, System.Func<float> getter, System.Action<float> setter, float min, float max, float step, System.Action<float> onSet = null)
        {
            _meta = meta;
            _getter = getter;
            _setter = setter;
            _min = min;
            _max = max;
            _step = step;
            _onSet = onSet;
        }

        public override float GetHeight(float width) => 40f;

        public override void Draw(Rect rect)
        {
            float value = _getter();

            var labelRect = new Rect(rect.x, rect.y, rect.width, 20f);
            GUI.Label(labelRect, $"{_meta.DisplayName}: {value:0.00}", UIStyle.LabelStyle);

            float sliderY = rect.y + 22f;
            var sliderRect = new Rect(rect.x, sliderY, rect.width, 12f);

            GUI.DrawTexture(sliderRect, UIStyle.GetSliderBg());

            float percent = Mathf.InverseLerp(_min, _max, value);
            float fillWidth = percent * sliderRect.width;
            var fillRect = new Rect(sliderRect.x, sliderRect.y, fillWidth, sliderRect.height);
            GUI.DrawTexture(fillRect, UIStyle.GetSliderFill());

            float knobX = Mathf.Clamp(sliderRect.x + fillWidth - 6f, sliderRect.x, sliderRect.x + sliderRect.width - 12f);
            var knobRect = new Rect(knobX, sliderRect.y - 6f, 12f, 24f);
            GUI.DrawTexture(knobRect, UIStyle.GetSliderKnob());

            Rect inputRect = new Rect(sliderRect.x - 6f, sliderRect.y - 6f, sliderRect.width + 12f, sliderRect.height + 12f);

            var e = Event.current;

            if (e.type == EventType.MouseDown && inputRect.Contains(e.mousePosition))
            {
                _isDragging = true;
                UpdateValueFromMouse(e.mousePosition.x, sliderRect);
                e.Use();
            }
            else if (e.type == EventType.MouseDrag && _isDragging)
            {
                UpdateValueFromMouse(e.mousePosition.x, sliderRect);
                e.Use();
            }
            else if (e.type == EventType.MouseUp && _isDragging)
            {
                _isDragging = false;
                if (_onSet != null)
                {
                    _onSet(_getter());
                }
                else
                {
                    UIContext.ShowNotification(UINotificationFormatter.Format(UINotificationEvent.Set, _meta.DisplayName, $"{value:0.00}"));
                }
                e.Use();
            }
        }

        private void UpdateValueFromMouse(float mouseX, Rect sliderRect)
        {
            float p = Mathf.InverseLerp(sliderRect.x, sliderRect.x + sliderRect.width, mouseX);
            p = Mathf.Clamp01(p);
            float newValue = (_max - _min) * p + _min;
            newValue = Mathf.Round(newValue / _step) * _step;
            _setter(newValue);
        }
    }
}

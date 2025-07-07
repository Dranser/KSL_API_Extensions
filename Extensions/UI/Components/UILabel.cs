using UnityEngine;

namespace KSL.API.Extensions.UI
{
    public class UILabel : UIElementBase
    {
        private readonly System.Func<string> _getter;
        private string _lastText = "";
        public UILabel(System.Func<string> getter)
        {
            _getter = getter;
        }

        public override float GetHeight(float width)
        {
            _lastText = _getter?.Invoke() ?? "";
            var content = new GUIContent(_lastText);
            return UIStyle.LabelStyle.CalcHeight(content, width);
        }

        public override void Draw(Rect rect)
        {
            GUI.Label(rect, _lastText, UIStyle.LabelStyle);
        }
    }
}

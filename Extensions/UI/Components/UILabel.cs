using UnityEngine;
using UnityEngine.UIElements;

namespace KSL.API.Extensions.UI
{
    public class UILabel : UIElementBase
    {
        private readonly string _text;

        public UILabel(string text)
        {
            _text = text;
        }

        public override float GetHeight(float width) => 20f;

        public override void Draw(Rect rect)
        {
            GUI.Label(rect, _text, UIStyle.LabelStyle);
        }
    }
}

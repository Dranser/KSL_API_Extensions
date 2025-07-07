using System.Collections.Generic;
using UnityEngine;

namespace KSL.API.Extensions.UI
{
    public class UIVerticalLayout : UIElementBase
    {
        private readonly List<IUIElement> _elements = new List<IUIElement>();
        private readonly float _spacing;

        public UIVerticalLayout(float spacing)
        {
            _spacing = spacing;
        }

        public void Add(IUIElement element)
        {
            _elements.Add(element);
        }

        public override float GetHeight(float width)
        {
            float height = 0f;

            for (int i = 0; i < _elements.Count; i++)
            {
                var elementHeight = _elements[i].GetHeight(width);
                height += elementHeight;
                if (i < _elements.Count - 1)
                    height += _spacing;
            }

            return height;
        }

        public override void Draw(Rect rect)
        {
            float y = rect.y;

            foreach (var element in _elements)
            {
                float h = element.GetHeight(rect.width);
                var r = new Rect(rect.x, y, rect.width, h);
                element.Draw(r);
                y += h + _spacing;
            }
        }
    }
}

using System.Collections.Generic;
using UnityEngine;

namespace KSL.API.Extensions.UI
{
    public class UIVerticalLayout : UIElementBase
    {
        private readonly List<IUIElement> _children = new List<IUIElement>();
        private readonly float _spacing;
        private readonly UIMetaAttribute _meta;

        public override UIMetaAttribute Meta => _meta;

        public UIVerticalLayout(float spacing = 6f, UIMetaAttribute meta = null)
        {
            _spacing = spacing;
            _meta = meta ?? new UIMetaAttribute("Vertical Layout");
        }

        public void Add(IUIElement element)
        {
            if (element != null)
                _children.Add(element);
        }

        public override float GetHeight(float width)
        {
            float height = 0f;
            for (int i = 0; i < _children.Count; i++)
            {
                height += _children[i].GetHeight(width);
                if (i < _children.Count - 1)
                    height += _spacing;
            }
            return height;
        }

        public override void Draw(Rect rect)
        {
            float y = rect.y;
            foreach (var el in _children)
            {
                float h = el.GetHeight(rect.width);
                el.Draw(new Rect(rect.x, y, rect.width, h));
                y += h + _spacing;
            }
        }
    }
}

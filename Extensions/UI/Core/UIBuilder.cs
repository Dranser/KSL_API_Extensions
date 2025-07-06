using System.Collections.Generic;

namespace KSL.API.Extensions.UI
{
    public class UIBuilder
    {
        private readonly List<IUIElement> _elements = new List<IUIElement>();

        public void Add(IUIElement element)
        {
            if (element != null)
                _elements.Add(element);
        }

        public void Clear()
        {
            _elements.Clear();
        }

        public float GetTotalHeight(float width)
        {
            float total = 0f;
            for (int i = 0; i < _elements.Count; i++)
            {
                total += _elements[i].GetHeight(width);
                if (i < _elements.Count - 1)
                    total += 6f;
            }
            return total;
        }

        public void Draw(UnityEngine.Rect area)
        {
            float y = area.y;
            foreach (var el in _elements)
            {
                float h = el.GetHeight(area.width);
                el.Draw(new UnityEngine.Rect(area.x, y, area.width, h));
                y += h + 6f;
            }
        }

        public void Refresh()
        {
            foreach (var el in _elements)
                el.Refresh();
        }
    }
}

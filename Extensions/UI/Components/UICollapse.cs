using UnityEngine;
using System.Collections.Generic;
using DG.Tweening;

namespace KSL.API.Extensions.UI
{
    public class UICollapse : UIElementBase
    {
        private readonly string _title;
        private readonly List<IUIElement> _children = new List<IUIElement>();
        private bool _expanded;

        private float _animatedHeight = 28f;
        private float _targetHeight = 28f;
        private Tween _heightTween;

        public UICollapse(string title, bool initiallyExpanded = true)
        {
            _title = title;
            _expanded = initiallyExpanded;
            _targetHeight = initiallyExpanded ? 200f : 28f;
            _animatedHeight = _targetHeight;
        }

        public void Add(IUIElement element)
        {
            if (element != null)
                _children.Add(element);
        }

        public override float GetHeight(float width)
        {
            return _animatedHeight;
        }

        public override void Draw(Rect rect)
        {
            var headerRect = new Rect(rect.x, rect.y, rect.width, 28f);
            if (GUI.Button(headerRect, "", UIStyle.SectionHeaderStyle))
                Toggle(rect.width);

            var labelRect = new Rect(rect.x + 8f, rect.y + 4f, rect.width - 32f, 20f);
            GUI.Label(labelRect, _title, UIStyle.LabelStyle);

            var arrow = _expanded ? "▼" : "►";
            var arrowRect = new Rect(rect.x + rect.width - 20f, rect.y + 4f, 16f, 20f);
            GUI.Label(arrowRect, arrow, UIStyle.LabelStyle);

            if (_animatedHeight <= 28f + 0.1f)
                return;

            var contentRect = new Rect(rect.x, rect.y + 26f, rect.width, _animatedHeight - 28f);
            GUI.Box(contentRect, GUIContent.none, UIStyle.SectionContentStyle);

            float y = rect.y + 26f + 4f;
            foreach (var el in _children)
            {
                float h = el.GetHeight(rect.width - 16f);
                el.Draw(new Rect(rect.x + 8f, y, rect.width - 16f, h));
                y += h + 6f;
            }
        }

        private void Toggle(float width)
        {
            _expanded = !_expanded;
            float newTarget = ComputeTargetHeight(width);

            _heightTween?.Kill();
            _heightTween = DOTween.To(
                () => _animatedHeight,
                x => _animatedHeight = x,
                newTarget,
                0.3f
            ).SetEase(Ease.OutCubic);
        }

        private float ComputeTargetHeight(float width)
        {
            if (!_expanded)
                return 28f;

            float height = 28f;
            foreach (var el in _children)
                height += el.GetHeight(width - 16f) + 6f;
            height += 8f;
            return height;
        }
    }
}

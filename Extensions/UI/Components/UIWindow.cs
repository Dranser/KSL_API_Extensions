using UnityEngine;

namespace KSL.API.Extensions.UI
{
    public class UIWindow
    {
        public string Id { get; }
        public string Title { get; }
        public Rect WindowRect;
        public bool IsVisible { get; set; } = true;

        private readonly UIBuilder _builder = new UIBuilder();
        private bool _needsRebuild = true;
        private bool _wasCentered = false;

        private float _animatedHeight;
        private bool _forceImmediateResize = false;

        public UIWindow(string id, string title, float width, float initialHeight)
        {
            Id = id;
            Title = title;
            WindowRect = new Rect(100, 100, width, initialHeight);
            _animatedHeight = initialHeight;
        }

        public void Build(System.Action<UIBuilder> builderCallback)
        {
            _builder.Clear();
            builderCallback?.Invoke(_builder);
            _needsRebuild = false;
        }

        public void RequestRebuild()
        {
            _needsRebuild = true;
            _forceImmediateResize = true;
        }

        public void Refresh()
        {
            _builder.Refresh();
        }

        public void Draw()
        {
            if (!IsVisible)
                return;

            if (!_wasCentered)
            {
                Center();
                _wasCentered = true;
            }

            WindowRect = GUI.Window(Title.GetHashCode(), WindowRect, DrawInternal, Title, UIStyle.WindowStyle);
        }

        private void DrawInternal(int id)
        {
            if (_needsRebuild)
                _needsRebuild = false;

            float contentWidth = WindowRect.width - 20f;
            float contentHeight = _builder.GetTotalHeight(contentWidth);
            float targetHeight = Mathf.Clamp(contentHeight + 40f, 100f, 1080f);

            if (_forceImmediateResize)
            {
                _animatedHeight = targetHeight;
                _forceImmediateResize = false;
            }
            else
            {
                _animatedHeight = Mathf.Lerp(_animatedHeight, targetHeight, 0.2f);
            }

            WindowRect.height = _animatedHeight;

            var contentRect = new UnityEngine.Rect(10f, 30f, contentWidth, contentHeight);
            UnityEngine.GUI.BeginGroup(contentRect);
            _builder.Draw(new UnityEngine.Rect(0, 0, contentWidth, contentHeight));
            UnityEngine.GUI.EndGroup();

            UnityEngine.GUI.DragWindow(new UnityEngine.Rect(0, 0, WindowRect.width, 24f));
        }

        private void Center()
        {
            WindowRect.x = (Screen.width - WindowRect.width) * 0.5f;
            WindowRect.y = (Screen.height - WindowRect.height) * 0.5f;
        }
    }
}

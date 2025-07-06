using UnityEngine;
using System;
using System.Reflection;

namespace KSL.API.Extensions.UI
{
    public class UIButton : UIElementBase
    {
        private readonly Action _onClick;
        private readonly MethodInfo _methodInfo;
        private readonly object _target;
        private readonly UIMetaAttribute _meta;

        public override UIMetaAttribute Meta => _meta;

        public UIButton(UIMetaAttribute meta, Action onClick)
        {
            _meta = meta;
            _onClick = onClick;
        }

        public UIButton(MethodInfo methodInfo, object target = null)
        {
            _methodInfo = methodInfo;
            _target = target;
            _meta = ResolveMeta(methodInfo);
        }

        public override float GetHeight(float width) => 28f;

        public override void Draw(Rect rect)
        {
            if (GUI.Button(rect, _meta.DisplayName, UIStyle.ButtonStyle))
            {
                if (_methodInfo != null)
                {
                    _methodInfo.Invoke(_target, null);
                    UIContext.ShowNotification(UINotificationFormatter.Format(UINotificationEvent.Invoked, _meta.DisplayName));
                }
                else
                {
                    _onClick?.Invoke();
                }
            }
        }

        private UIMetaAttribute ResolveMeta(MethodInfo method)
        {
            var attr = method.GetCustomAttribute<UIMetaAttribute>();
            if (attr != null)
                return attr;

            return new UIMetaAttribute(method.Name);
        }
    }
}

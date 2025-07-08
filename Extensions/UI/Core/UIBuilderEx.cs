using System;

namespace KSL.API.Extensions.UI
{
    public static class UIBuilderEx
    {
        public static void Collapse(this UIBuilder builder, string title, Action<UIVerticalLayout> content, bool expanded = false, float spacing = 6f)
        {
            var layout = new UIVerticalLayout(spacing);
            content?.Invoke(layout);

            var section = new UICollapse(title, expanded);
            section.Add(layout);

            builder.Add(section);
        }

        public static void Label(this UIVerticalLayout layout, Func<string> getter)
        {
            layout.Add(new UILabel(getter));
        }

        public static void Button(this UIVerticalLayout layout, string label, Action onClick)
        {
            layout.Add(new UIButton(new UIMetaAttribute(label), onClick));
        }

        public static void Slider(this UIVerticalLayout layout, string label, Func<float> getter, Action<float> setter,
                                  float min, float max, float step = 1f, Action<float> onSet = null)
        {
            layout.Add(new UISlider(new UIMetaAttribute(label), getter, setter, min, max, step, onSet));
        }

        public static void Toggle(this UIVerticalLayout layout, string label, Func<bool> getter, Action<bool> setter)
        {
            layout.Add(new UIToggle(new UIMetaAttribute(label), getter, setter));
        }

        public static void Spacer(this UIVerticalLayout layout, float height = 10f)
        {
            layout.Add(new UISpacer(height));
        }

        public static void AddLabel(this UIBuilder builder, Func<string> getter)
        {
            builder.Add(new UILabel(getter));
        }

        public static void AddButton(this UIBuilder builder, string label, Action onClick)
        {
            builder.Add(new UIButton(new UIMetaAttribute(label), onClick));
        }

        public static void AddSpacer(this UIBuilder builder, float height = 10f)
        {
            builder.Add(new UISpacer(height));
        }
    }
}

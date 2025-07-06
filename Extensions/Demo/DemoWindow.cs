using KSL.API.Extensions.UI;

[UIWindow]
public class DemoWindow
{
    private static bool _toggleValue = true;
    private static float _sliderValue = 0.5f;

    [UIMeta("Reset Slider", "Resets the slider to zero")]
    public static void ResetSlider()
    {
        _sliderValue = 0f;
    }

    public static UIWindow Create()
    {
        var window = new UIWindow("demo", "Demo Window", 400, 600);

        window.Build(builder =>
        {
            var section1 = new UICollapse("Main Section", initiallyExpanded: true);
            var layout1 = new UIVerticalLayout(8f);

            layout1.Add(new UILabel("This is a label"));

            layout1.Add(new UIButton(
                new UIMetaAttribute("Click me", "Simple button"),
                () =>
                {

                }
            ));

            layout1.Add(new UIButton(
                typeof(DemoWindow).GetMethod(nameof(ResetSlider))
            ));

            layout1.Add(new UIToggle(
                new UIMetaAttribute("SWAGA", "Super toggle switch"),
                () => _toggleValue,
                v => _toggleValue = v
            ));

            layout1.Add(new UISlider(
                new UIMetaAttribute("Adjust Value", "Adjusts a numeric value"),
                () => _sliderValue,
                v => _sliderValue = v,
                0f, 10f, 0.1f
            ));

            section1.Add(layout1);
            builder.Add(section1);

            var section2 = new UICollapse("Secondary Section", initiallyExpanded: false);
            var layout2 = new UIVerticalLayout(8f);
            layout2.Add(new UILabel("Collapsed by default."));
            section2.Add(layout2);
            builder.Add(section2);

            var section3 = new UICollapse("Other Settings", initiallyExpanded: false);
            var layout3 = new UIVerticalLayout(8f);
            layout3.Add(new UILabel("More controls here."));
            section3.Add(layout3);
            builder.Add(section3);
        });

        return window;
    }
}

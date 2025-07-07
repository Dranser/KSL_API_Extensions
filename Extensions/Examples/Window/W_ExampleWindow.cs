using KSL.API.Extensions;
using KSL.API.Extensions.UI;

namespace ExampleMod
{
    [Example] // REMOVE THIS ATTRIBUTE IF YOU WANT TO USE THE FEATURE
    [UIWindow]
    [DrawCondition(DrawConditionType.ActiveSession)]
    public class W_ExampleWindow
    {
        public static UIWindow Create()
        {
            var window = new UIWindow("example", "Example Window", 320, 180);

            window.Build(builder =>
            {
                var section = new UICollapse("Example Section", true);
                var layout = new UIVerticalLayout(6f);

                // Add UI elements here, e.g.,
                // layout.Add(new UILabel(() => "Hello World"));

                section.Add(layout);
                builder.Add(section);
            });

            return window;
        }
    }
}

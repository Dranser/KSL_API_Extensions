using System.Reflection;

namespace KSL.API.Extensions.UI
{
    [ModFeature]
    public class UIFeature : FeatureBase, IModFeatureLifecycle
    {
        public override string Id => "system.ui";

        public override void Init()
        {
            UIContext.Init(Assembly.GetExecutingAssembly());
        }

        public void Update() { }

        public void Draw()
        {
            UIContext.OnGUI();
        }
    }
}

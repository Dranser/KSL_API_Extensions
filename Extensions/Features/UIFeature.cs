using System.Reflection;

namespace KSL.API.Extensions.UI
{
    [ModFeature]
    [DrawCondition(DrawConditionType.ActiveSession)]
    public class UIFeature : FeatureBase, IModFeatureLifecycle
    {
        public override string Id => "system.ui";
        public override bool IsSystem => true;

        public override void OnInit()
        {
            UIContext.Init();
            UIContext.DiscoverUI(Assembly.GetExecutingAssembly());
        }

        public void Update()
        {
        }

        public void Draw()
        {
            UIContext.OnGUI();
        }
    }
}

namespace KSL.API.Extensions.UI
{
    [ModFeature]
    [DrawCondition(DrawConditionType.None)]
    public class UIFeature : FeatureBase, IModFeatureLifecycle
    {
        public override string Id => "system.ui";
        public override bool IsSystem => true;

        public override void OnInit()
        {
            UIContext.Init();
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

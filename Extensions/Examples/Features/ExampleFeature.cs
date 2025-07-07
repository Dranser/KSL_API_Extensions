using KSL.API.Extensions;

namespace ExampleMod
{
    [Example] // REMOVE THIS ATTRIBUTE IF YOU WANT TO USE THE FEATURE
    [ModFeature]
    [DrawCondition(DrawConditionType.CarReadyOnTrack)]
    public class ExampleFeature : FeatureBase, IModFeatureLifecycle
    {
        public override string Id => "example.feature";

        public override void OnInit()
        {
            // Initialization logic here
        }

        public override void OnShutdown()
        {
            // Cleanup logic here
        }

        public void Draw()
        {
            // Drawing logic here
        }

        public void Update()
        {
            // Update logic here
        }
    }
}

namespace KSL.API.Extensions
{
    public class MainDispatcherFeature : FeatureBase
    {
        public override string Id => "system.dispatcher";

        public override void Init()
        {
            MainThreadDispatcher.Init();
        }
    }
}

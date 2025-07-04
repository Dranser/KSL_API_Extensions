using KSL.API.Extensions;

public class MainDispatcherFeature : ModFeatureBase
{
    public override string Id => "system.dispatcher";
    public override bool IsSystem => true;

    public override void OnInit()
    {
        MainThreadDispatcher.Init();
    }
}

using KSL.API.Extensions;

public class GameStateTrackerFeature : ModFeatureBase
{
    public override string Id => "system.gamestate";
    public override bool IsSystem => true;

    public override void OnInit()
    {
        GameStateTracker.Init();
    }
}

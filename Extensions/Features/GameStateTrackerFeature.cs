namespace KSL.API.Extensions
{
    public class GameStateTrackerFeature : FeatureBase
    {
        public override string Id => "system.gamestate";
        public override void Init()
        {
            GameStateTracker.Init();
        }
    }
}

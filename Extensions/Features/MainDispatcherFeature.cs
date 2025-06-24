namespace KSL.API.Extensions
{
    public class MainDispatcherFeature : IModFeature
    {
        public string Id => "system.dispatcher";
        public bool Enabled => true;
        public bool IsSystem => true;

        public void OnInit()
        {
            MainThreadDispatcher.Init();
            GameStateTracker.Init();
        }

        public void OnReady() { }

        public void OnShutdown() { }
    }
}

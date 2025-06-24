namespace KSL.API.Extensions
{
    public interface IModFeature
    {
        string Id { get; }
        bool Enabled { get; }
        bool IsSystem { get; }

        void OnInit();
        void OnReady();
        void OnShutdown();
    }
}

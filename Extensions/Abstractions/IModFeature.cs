namespace KSL.API.Extensions
{
    /// <summary>
    /// Base contract for a mod feature. Includes <see cref="IModFeatureLifecycle"/> so
    /// <see cref="Update"/> and <see cref="Draw"/> are always available.
    /// </summary>
    public interface IModFeature : IModFeatureLifecycle
    {
        string Id { get; }
        bool Enabled { get; }
        bool IsSystem { get; }

        void OnInit();
        void OnReady();
        void OnShutdown();
    }
}

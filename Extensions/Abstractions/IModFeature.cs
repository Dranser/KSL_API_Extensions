namespace KSL.API.Extensions
{
    public interface IModFeature
    {
        string Id { get; }
        bool Enabled { get; set; }

        void Init();
        void Ready();
        void Shutdown();
    }
}

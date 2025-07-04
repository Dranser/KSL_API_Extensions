namespace KSL.API.Extensions
{
    /// <summary>
    /// Convenience base class for implementing <see cref="IModFeature"/>.
    /// </summary>
    public abstract class ModFeatureBase : IModFeature
    {
        public abstract string Id { get; }
        public virtual bool Enabled => true;
        public virtual bool IsSystem => false;

        public virtual void OnInit() { }
        public virtual void OnReady() { }
        public virtual void OnShutdown() { }
        public virtual void Update() { }
        public virtual void Draw() { }
    }
}

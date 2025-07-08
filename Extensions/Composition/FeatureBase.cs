namespace KSL.API.Extensions
{
    public abstract class FeatureBase : IModFeature
    {
        public abstract string Id { get; }
        public virtual bool Enabled { get; set; } = true;

        public virtual void Init() { }
        public virtual void Ready() { }
        public virtual void Shutdown() { }
    }
}

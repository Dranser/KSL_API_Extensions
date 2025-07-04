namespace KSL.API.Extensions
{
    public abstract class FeatureBase : IModFeature
    {
        public abstract string Id { get; }
        public virtual bool Enabled => true;
        public virtual bool IsSystem => false;

        public virtual void OnInit() { }
        public virtual void OnReady() { }
        public virtual void OnShutdown() { }
    }
}

namespace KSL.API.Extensions.UI
{
    public abstract class UIElementBase : IUIElement
    {
        public virtual UIMetaAttribute Meta { get; }
        public abstract float GetHeight(float width);
        public abstract void Draw(UnityEngine.Rect rect);
        public virtual void Refresh() { }
    }
}

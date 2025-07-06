namespace KSL.API.Extensions.UI
{
    public interface IUIElement
    {
        UIMetaAttribute Meta { get; }
        float GetHeight(float width);
        void Draw(UnityEngine.Rect rect);
        void Refresh();
    }
}

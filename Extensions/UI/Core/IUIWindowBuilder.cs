using System;

namespace KSL.API.Extensions.UI
{
    public interface IUIWindowBuilder
    {
        void Build(UIBuilder builder);
        string Id { get; }
        string Title { get; }
        float Width { get; }
        float Height { get; }
        Func<bool> Condition { get; }
    }
}

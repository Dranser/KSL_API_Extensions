using KSL.API.Extensions.UI;
using System;
using UnityEngine;

public class HUDView
{
    public string Id { get; }
    public Rect Area;
    private readonly UIBuilder _builder = new UIBuilder();

    public HUDView(string id, float x, float y, float width)
    {
        Id = id;
        Area = new Rect(x, y, width, 0);
    }

    public void Build(Action<UIBuilder> builderCallback)
    {
        _builder.Clear();
        builderCallback?.Invoke(_builder);
        float height = _builder.GetTotalHeight(Area.width);
        Area.height = height;
    }

    public void Draw()
    {
        _builder.Draw(Area);
    }
}

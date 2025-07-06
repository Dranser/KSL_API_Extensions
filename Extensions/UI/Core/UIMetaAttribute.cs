using System;

[AttributeUsage(AttributeTargets.All)]
public class UIMetaAttribute : Attribute
{
    public string DisplayName { get; }
    public string Description { get; }

    public UIMetaAttribute(string displayName, string description = null)
    {
        DisplayName = displayName;
        Description = description;
    }
}

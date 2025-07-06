public static class UINotificationFormatter
{
    public static string Format(UINotificationEvent evt, string displayName, string state = null)
    {
        if (string.IsNullOrEmpty(state))
            return $"[{evt}] {displayName}";

        return $"[{evt}] {displayName}: {state}";
    }
}

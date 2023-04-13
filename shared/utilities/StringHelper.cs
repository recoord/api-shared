public static class StringHelper
{
    public static string GetErrorsAsString(IDictionary<string, string[]> errors)
    {
        var items = errors.Select(kvp => $"[{kvp.Key}: ({String.Join(", ", kvp.Value.Select(s => s.ToString()).ToArray())})]");
        return string.Join(", ", items);
    }
}
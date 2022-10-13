public static class JobKindTypesV1
{
    public static readonly string NoKind = "noKind";
    public static readonly string StackedRendering = "stackedRendering";

    private static string[]? _allTypes;
    public static string[] GetAllTypes()
    {
        if (_allTypes == null)
        {
            var l = new List<string>
            {
                NoKind,
                StackedRendering
            };
            _allTypes = l.ToArray();
        }
        return _allTypes;
    }
}
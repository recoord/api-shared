public static class JobKindTypesV1
{
    public static readonly string StackedRendering = "stackedRendering";

    private static List<string>? _allTypes;
    public static IList<string> GetAllTypes()
    {
        if (_allTypes == null)
        {
            _allTypes = new List<string>
            {
                StackedRendering
            };
        }
        return _allTypes;
    }
}
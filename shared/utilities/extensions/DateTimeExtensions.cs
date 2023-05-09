public static class DateTimeExtensions
{
    public static string ToUniversalIso8601(this DateTime dateTime)
    {
        return dateTime.ToUniversalTime().ToString("u").Replace(" ", "T");
    }
}
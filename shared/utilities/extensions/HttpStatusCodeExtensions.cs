namespace Api.Shared;

using System.Net;

public static class HttpStatusCodeExtensions
{
    public static bool IsSuccessStatusCode(this HttpStatusCode statusCode)
    {
        var asInt = (int)statusCode;
        return asInt >= 200 && asInt <= 299;
    }

    public static bool ShouldRetry(this HttpStatusCode statusCode)
    {
        var asInt = (int)statusCode;
        return asInt == 408 ||
            asInt == 425 ||
            asInt == 429 ||
            asInt == 502 ||
            asInt == 503 ||
            asInt == 504;
    }
}
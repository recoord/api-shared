namespace Api.Shared;

public static class UrlHelper
{
    public static string? GenerateS3Uri(string? bucket, string? key)
    {
        if (bucket == null || key == null)
        {
            return null;
        }

        return $"s3://{bucket}/{key}";
    }

    public static string? GenerateUrl(string? bucket, string? key)
    {
        if (bucket == null || key == null)
        {
            return null;
        }

        return $"https://{bucket}.s3.amazonaws.com/{key}";
    }
}
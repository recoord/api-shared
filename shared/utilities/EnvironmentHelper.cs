public static class EnvironmentHelper
{
    public static bool IsDevEnvironment()
    {
        return (Environment.GetEnvironmentVariable("VEO_ENV") ?? "dev").ToLowerInvariant() == "dev";
    }

    public static bool IsStagEnvironment()
    {
        return (Environment.GetEnvironmentVariable("VEO_ENV") ?? "dev").ToLowerInvariant() == "stag";
    }

    public static bool IsProdEnvironment()
    {
        return (Environment.GetEnvironmentVariable("VEO_ENV") ?? "dev").ToLowerInvariant() == "prod";
    }
}
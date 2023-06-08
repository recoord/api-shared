namespace Api.Shared;

using Microsoft.Extensions.Configuration;

public abstract class ACustomMetric
{
    private readonly string _serviceName;

    public ACustomMetric(IConfiguration config)
    {
        _serviceName = config["Application:Name"] ?? "service-name-not-present";
    }

    protected string GenerateServiceName(string name)
    {
        return $"{_serviceName}_{name}";
    }
}

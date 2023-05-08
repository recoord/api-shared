using DotNet.Testcontainers.Builders;
using DotNet.Testcontainers.Containers;
using Xunit;

public class LocalStackFixture : IAsyncLifetime
{
    private readonly IContainer _localStackContainer;

    public LocalStackFixture()
    {
        var localStackBuilder = new ContainerBuilder()
            .WithImage("localstack/localstack")
            .WithCleanUp(true)
            .WithEnvironment("DEFAULT_REGION", "eu-west-1")
            .WithEnvironment("SERVICES", "sqs,s3,events")
            .WithEnvironment("DOCKER_HOST", "unix:///var/run/docker.sock")
            .WithEnvironment("DEBUG", "1")
            .WithPortBinding(4567, 4566);

        _localStackContainer = localStackBuilder.Build();
    }
    public async Task InitializeAsync()
    {
        await _localStackContainer.StartAsync();
    }

    public async Task DisposeAsync()
    {
        await _localStackContainer.StopAsync();
    }
}
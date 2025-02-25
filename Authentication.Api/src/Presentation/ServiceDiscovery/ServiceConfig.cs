namespace Presentation.ServiceDiscovery;

internal sealed class ServiceConfig
{
    public required string Id { get; init; }
    public required string Name { get; init; }
    public required string ApiHost { get; init; }
    public required int Port { get; init; }
    public required Uri ConsulUrl { get; init; }
}

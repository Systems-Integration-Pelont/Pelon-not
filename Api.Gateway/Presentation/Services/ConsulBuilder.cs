using Consul;
using Ocelot.Logging;
using Ocelot.Provider.Consul;
using Ocelot.Provider.Consul.Interfaces;

namespace Presentation.Services;

internal sealed class ConsulBuilder(
    IHttpContextAccessor contextAccessor,
    IConsulClientFactory clientFactory,
    IOcelotLoggerFactory loggerFactory
) : DefaultConsulServiceBuilder(contextAccessor, clientFactory, loggerFactory)
{
    protected override string GetDownstreamHost(ServiceEntry entry, Node node)
    {
        return entry.Service.Address;
    }
}

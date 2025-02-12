using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Presentation.Configurations;

internal static class AlterUpstream
{
    public static string AlterUpstreamSwaggerJson(HttpContext _, string swaggerJson)
    {
        var swagger = JObject.Parse(swaggerJson);

        return swagger.ToString(Formatting.Indented);
    }
}

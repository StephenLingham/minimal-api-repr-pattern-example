using System.Reflection;

namespace MinimalApiReprPattern.Endpoints;

public static class EndpointExtensions
{
    public static void MapEndpoints(this WebApplication app, Assembly? assembly = null)
    {
        assembly ??= Assembly.GetExecutingAssembly();

        var endpointTypes = assembly.GetTypes()
            .Where(t => t.IsClass && 
                        !t.IsAbstract && 
                        t.GetInterfaces().Contains(typeof(IEndpoint)));

        foreach (var endpointType in endpointTypes)
        {
            var mapMethod = endpointType.GetMethod("Map", BindingFlags.Public | BindingFlags.Static);
            mapMethod?.Invoke(null, [app]);
        }
    }
}

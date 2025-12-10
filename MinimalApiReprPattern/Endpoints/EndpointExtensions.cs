using System.Reflection;

namespace MinimalApiReprPattern.Endpoints;

/// <summary>
/// Extension methods for auto-discovering and mapping endpoints
/// </summary>
public static class EndpointExtensions
{
    /// <summary>
    /// Automatically discovers and maps all IEndpoint implementations in the specified assembly
    /// </summary>
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

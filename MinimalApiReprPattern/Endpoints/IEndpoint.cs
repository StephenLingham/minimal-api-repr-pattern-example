namespace MinimalApiReprPattern.Endpoints;

/// <summary>
/// Interface for self-contained endpoints following the REPR pattern.
/// Each endpoint is responsible for mapping itself to the application.
/// </summary>
public interface IEndpoint
{
    static abstract void Map(IEndpointRouteBuilder app);
}

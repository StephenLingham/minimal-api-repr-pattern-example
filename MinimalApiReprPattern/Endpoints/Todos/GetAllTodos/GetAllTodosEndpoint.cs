using Microsoft.EntityFrameworkCore;
using MinimalApiReprPattern.Data;

namespace MinimalApiReprPattern.Endpoints.Todos.GetAllTodos;

public class GetAllTodosEndpoint : IEndpoint
{
    public static void Map(IEndpointRouteBuilder app)
    {
        app.MapGet("/api/todos", Handle)
            .WithName("GetAllTodos")
            .WithTags("Todos")
            .Produces<GetAllTodosResponse>(StatusCodes.Status200OK);
    }

    public static async Task<IResult> Handle(TodoDbContext db)
    {
        var todos = await db.Todos
            .Select(t => new TodoDto(t.Id, t.Title, t.IsComplete))
            .ToListAsync();
        
        return Results.Ok(new GetAllTodosResponse(todos));
    }
}

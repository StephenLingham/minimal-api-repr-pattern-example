using MinimalApiReprPattern.Data;

namespace MinimalApiReprPattern.Endpoints.Todos;

public class GetTodoById : IEndpoint
{
    public record GetTodoByIdResponse(int Id, string Title, bool IsComplete);

    public static void Map(IEndpointRouteBuilder app)
    {
        app.MapGet("/api/todos/{id:int}", Handle)
            .WithName("GetTodoById")
            .WithTags("Todos")
            .Produces<GetTodoByIdResponse>(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status404NotFound);
    }

    public static async Task<IResult> Handle(int id, TodoDbContext db)
    {
        var todo = await db.Todos.FindAsync(id);
        
        if (todo is null)
            return Results.NotFound();
        
        return Results.Ok(new GetTodoByIdResponse(todo.Id, todo.Title, todo.IsComplete));
    }
}

using MinimalApiReprPattern.Data;

namespace MinimalApiReprPattern.Endpoints.Todos;

public class DeleteTodo : IEndpoint
{
    public static void Map(IEndpointRouteBuilder app)
    {
        app.MapDelete("/api/todos/{id:int}", Handle)
            .WithName("DeleteTodo")
            .WithTags("Todos")
            .Produces(StatusCodes.Status204NoContent)
            .ProducesProblem(StatusCodes.Status404NotFound);
    }

    public static async Task<IResult> Handle(int id, TodoDbContext db)
    {
        var todo = await db.Todos.FindAsync(id);
        
        if (todo is null)
            return Results.NotFound();
        
        db.Todos.Remove(todo);
        await db.SaveChangesAsync();
        
        return Results.NoContent();
    }
}

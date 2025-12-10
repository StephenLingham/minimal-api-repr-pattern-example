using MinimalApiReprPattern.Data;

namespace MinimalApiReprPattern.Endpoints.Todos;

public class UpdateTodo : IEndpoint
{
    public record UpdateTodoRequest(string Title, bool IsComplete);
    public record UpdateTodoResponse(int Id, string Title, bool IsComplete);

    public static void Map(IEndpointRouteBuilder app)
    {
        app.MapPut("/api/todos/{id:int}", Handle)
            .WithName("UpdateTodo")
            .WithTags("Todos")
            .Accepts<UpdateTodoRequest>("application/json")
            .Produces<UpdateTodoResponse>(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status404NotFound)
            .ProducesValidationProblem();
    }

    public static async Task<IResult> Handle(int id, UpdateTodoRequest request, TodoDbContext db)
    {
        if (string.IsNullOrWhiteSpace(request.Title))
            return Results.BadRequest(new { Message = "Title is required" });
        
        var todo = await db.Todos.FindAsync(id);
        
        if (todo is null)
            return Results.NotFound();
        
        todo.Title = request.Title;
        todo.IsComplete = request.IsComplete;
        
        await db.SaveChangesAsync();
        
        return Results.Ok(new UpdateTodoResponse(todo.Id, todo.Title, todo.IsComplete));
    }
}

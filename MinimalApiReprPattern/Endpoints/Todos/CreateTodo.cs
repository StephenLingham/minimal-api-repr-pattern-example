using MinimalApiReprPattern.Data;

namespace MinimalApiReprPattern.Endpoints.Todos;

public class CreateTodo : IEndpoint
{
    public record CreateTodoRequest(string Title);
    public record CreateTodoResponse(int Id, string Title, bool IsComplete);

    public static void Map(IEndpointRouteBuilder app)
    {
        app.MapPost("/api/todos", Handle)
            .WithName("CreateTodo")
            .WithTags("Todos")
            .Accepts<CreateTodoRequest>("application/json")
            .Produces<CreateTodoResponse>(StatusCodes.Status201Created)
            .ProducesValidationProblem();
    }

    public static async Task<IResult> Handle(CreateTodoRequest request, TodoDbContext db)
    {
        if (string.IsNullOrWhiteSpace(request.Title))
        {
            return Results.BadRequest(new { Message = "Title is required" });
        }
        
        var todo = new TodoItem { Title = request.Title, IsComplete = false };
        
        db.Todos.Add(todo);
        await db.SaveChangesAsync();
        
        return Results.Created($"/api/todos/{todo.Id}", new CreateTodoResponse(todo.Id, todo.Title, todo.IsComplete));
    }
}

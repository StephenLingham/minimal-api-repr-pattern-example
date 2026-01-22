namespace MinimalApiReprPattern.Endpoints.Todos.GetTodoById;

public record GetTodoByIdResponse(int Id, string Title, bool IsComplete);

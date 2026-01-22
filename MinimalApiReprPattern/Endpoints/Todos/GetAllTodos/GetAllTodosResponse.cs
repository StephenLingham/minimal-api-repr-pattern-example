namespace MinimalApiReprPattern.Endpoints.Todos.GetAllTodos;

public record GetAllTodosResponse(IEnumerable<TodoDto> Todos);

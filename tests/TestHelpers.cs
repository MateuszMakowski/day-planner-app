using TodoApi.Models;

namespace TodoApi.tests;
static class TestHelpers
{
    internal static List<Todo> InitialTodos()
    {
        List<Todo> todos = new()
        {
            new Todo()
            {
                Id = "1",
                Name = "Eat",
                CompletionFlag = false,
                Date = ""
            },
            new Todo()
            {
                Id = "2",
                Name = "Sleep",
                CompletionFlag = false,
                Date = ""
            }
        };
        return todos;
    }
}

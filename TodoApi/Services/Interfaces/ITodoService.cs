using TodoApi.Models;

namespace TodoApi.Services.Interfaces;

public interface ITodoService
{
    public Task<List<Todo>> GetAsync();
    public Task CreateAsync(Todo todo);
    public Task UpdateNameAsync(string id, string name);
    public Task UpdateCompletionFlagAsync(string id, bool completed);
    public Task RemoveAsync(string id);
}
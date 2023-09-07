using Microsoft.Extensions.Options;
using MongoDB.Driver;
using TodoApi.Models;
using TodoApi.Services.Interfaces;

namespace TodoApi.Services;

public class TodoService : ITodoService
{
    private readonly IMongoCollection<Todo> _tasksCollection;

    public TodoService(IOptions<TodoDatabaseSettings> todoDatabaseSettings)
    {
        var mongoClient = new MongoClient(todoDatabaseSettings.Value.ConnectionString);
        var mongoDatabase = mongoClient.GetDatabase(todoDatabaseSettings.Value.DatabaseName);
        _tasksCollection = mongoDatabase.GetCollection<Todo>(todoDatabaseSettings.Value.TasksCollectionName);
    }
    public async Task<List<Todo>> GetAsync() => await _tasksCollection.Find(_ => true).ToListAsync();

    public async Task CreateAsync(Todo todo)
    {
        todo.Date = DateTime.Now.ToString("yyyy'-'MM'-'dd");
        await _tasksCollection.InsertOneAsync(todo);
    }

    public async Task UpdateNameAsync(string id, string name) =>
        await _tasksCollection.UpdateOneAsync(x => x.Id == id, Builders<Todo>.Update.Set("Name", name));

    public async Task UpdateCompletionFlagAsync(string id, bool completed)
    {
        var modifyDate = DateTime.Now.ToString("yyyy'-'MM'-'dd");
        await _tasksCollection.UpdateOneAsync(x => x.Id == id, Builders<Todo>.Update.Set("CompletionFlag", completed).Set("Date", modifyDate));
    }

    public async Task RemoveAsync(string id) => await _tasksCollection.DeleteOneAsync(x => x.Id == id);
}

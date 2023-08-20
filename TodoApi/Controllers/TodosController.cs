using Microsoft.AspNetCore.Mvc;
using TodoApi.Models;
using TodoApi.Services;
using TodoApi.Services.Interfaces;

namespace TodoApi.Controllers;

[ApiController]
[Route("[Controller]")]
public class TodosController : ControllerBase
{
    private readonly ITodoService _todoService;

    public TodosController(ITodoService todoService) => _todoService = todoService;

    [HttpGet]
    public async Task<List<Todo>> Get() => await _todoService.GetAsync();

    [ActionName("")]
    [HttpGet("{id:length(24)}")]
    public async Task<Todo> Get(string id)
    {
        var todo = await _todoService.GetAsync(id);

        return todo;
    }

    [HttpPost]
    public async Task<IActionResult> Create(Todo newTodo)
    {
        await _todoService.CreateAsync(newTodo);
        return CreatedAtAction(nameof(Get), new { id = newTodo.Id }, newTodo);
    }

    [HttpPut("Replace")]
    public async Task<IActionResult> Update(string id, Todo updatedTodo)
    {
        await _todoService.UpdateAsync(id, updatedTodo);
        return NoContent();
    }

    [HttpPut("Name")]
    public async Task<IActionResult> UpdateName(string id, string name)
    {
        await _todoService.UpdateNameAsync(id, name);
        return NoContent();
    }

    [HttpPut("CompletionFlag")]
    public async Task<IActionResult> UpdateCompletionFlag(string id, bool completed)
    {
        await _todoService.UpdateCompletionFlagAsync(id, completed);
        return NoContent();
    }

    [HttpDelete]
    public async Task<IActionResult> Delete(string id)
    {
        await _todoService.RemoveAsync(id);
        return NoContent();
    }
}




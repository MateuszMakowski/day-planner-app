using NSubstitute;
using TodoApi.Controllers;
using TodoApi.Models;
using TodoApi.Services.Interfaces;
using Xunit;

namespace TodoApi.tests;

public class TodoApiTests
{
    private readonly ITodoService _mockService;
    private readonly TodosController _todoController;

    public TodoApiTests()
    {
        _mockService = Substitute.For<ITodoService>();
        _todoController = new TodosController(_mockService);
    }
    [Fact]
    public async void Get_ShouldReturnListOfAllTodos()
    {
        // Arrange
        _mockService.GetAsync().Returns(TestHelpers.InitialTodos());

        // Act
        var result = await _todoController.Get();

        // Assert
        Assert.Equal(2, result.Count);
    }

    [Fact]
    public async void Create_ShouldReturnCreatedTodo()
    {
        var date = DateTime.Now.ToString("yyyy'-'MM'-'dd");
        // Arrange
        var newTodo = new Todo()
        {
            Id = "1",
            Name = "Sleep",
            CompletionFlag = false,
            Date = date
        };

        // Act
        await _todoController.Create(newTodo);

        // Assert
        await _mockService.Received(1).CreateAsync(newTodo);
    }

    [Fact]
    public async void Update_ShouldUpdateName()
    {
        // Arrange
        var name = "Sleep";

        // Act
        await _todoController.UpdateName("1", name);

        // Assert
        await _mockService.Received(1).UpdateNameAsync("1", name);
    }

    [Fact]
    public async void Update_ShouldUpdateCompletionFlag()
    {
        // Arrange
        var completed = true;

        // Act
        await _todoController.UpdateCompletionFlag("1", completed);

        // Assert
        await _mockService.Received(1).UpdateCompletionFlagAsync("1", completed);
    }

    [Fact]
    public async void Delete_ShouldRemoveExistingTodo()
    {
        // Arrange

        // Act
        await _todoController.Delete("1");

        // Assert
        await _mockService.Received(1).RemoveAsync("1");
    }
}
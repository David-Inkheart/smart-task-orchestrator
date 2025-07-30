using Application.Interfaces;
using Domain.Entities;
using Domain.ValueObjects;
using Infrastructure.Services;
using Xunit;

namespace Application.Tests.Services;

public class TaskServiceTests
{
  private readonly InMemoryTaskService _taskService;

  public TaskServiceTests()
  {
    _taskService = new InMemoryTaskService();
  }

  [Fact]
  public async Task CreateTaskAsync_ShouldCreateTaskWithCorrectValues()
  {
    // Arrange
    var title = "Write Unit Tests";
    var description = "We write tests before logic.";
    var priority = Priority.High;

    // Act
    var task = await _taskService.CreateTaskAsync(title, description, priority);

    // Assert
    Assert.NotNull(task);
    Assert.Equal(title, task.Title);
    Assert.Equal(description, task.Description);
    Assert.Equal(priority, task.Priority);
    Assert.NotEqual(Guid.Empty, task.Id);
    Assert.True((DateTime.UtcNow - task.CreatedAt).TotalSeconds < 5);
  }

  [Fact]
  public async Task GetTaskByIdAsync_ShouldReturnTask_WhenTaskExists()
  {
    // Arrange
    var task = await _taskService.CreateTaskAsync("Learn TDD", "Write test before code", Priority.Medium);

    // Act
    var fetchedTask = await _taskService.GetTaskByIdAsync(task.Id);

    // Assert
    Assert.NotNull(fetchedTask);
    Assert.Equal(task.Id, fetchedTask!.Id);
  }

  [Fact]
  public async Task GetTaskByIdAsync_ShouldReturnNull_WhenTaskDoesNotExist()
  {
    // Act
    var result = await _taskService.GetTaskByIdAsync(Guid.NewGuid());

    // Assert
    Assert.Null(result);
  }

  [Fact]
  public async Task UpdateTaskAsync_ShouldModifyTaskFields()
  {
    // Arrange
    var task = await _taskService.CreateTaskAsync("Old Title", "Old Description", Priority.Low);

    var newTitle = "New Title";
    var newDescription = "New Description";
    var newPriority = Priority.Critical;

    // Act
    await _taskService.UpdateTaskAsync(task.Id, newTitle, newDescription, newPriority);
    var updatedTask = await _taskService.GetTaskByIdAsync(task.Id);

    // Assert
    Assert.Equal(newTitle, updatedTask!.Title);
    Assert.Equal(newDescription, updatedTask.Description);
    Assert.Equal(newPriority, updatedTask.Priority);
  }
}

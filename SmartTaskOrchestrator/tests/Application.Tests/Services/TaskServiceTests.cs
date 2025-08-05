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
    var aiService = new FakeAIService();
    _taskService = new InMemoryTaskService(aiService);
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

  [Fact]
  public async Task AssignAgentToTask_AssignsAgentCorrectly()
  {
    // Arrange
    var aiService = new FakeAIService();
    var service = new InMemoryTaskService(aiService);
    var task = await service.CreateTaskAsync("Write tests", "We must TDD", Priority.Medium);
    var agentId = "agent-007";

    // Act
    await service.AssignAgentToTask(task.Id, agentId);
    var updatedTask = await service.GetTaskByIdAsync(task.Id);

    // Assert
    Assert.NotNull(updatedTask);
    Assert.Equal(agentId, updatedTask!.AssignedAgent);
  }


  [Fact]
  public async Task AssignAgentToTask_InvalidTaskId_DoesNothing()
  {
    // Arrange
    var aiService = new FakeAIService();
    var service = new InMemoryTaskService(aiService);
    var invalidId = Guid.NewGuid(); // Not in store
    var agentId = "ghost-agent";

    // Act
    var ex = await Record.ExceptionAsync(() => service.AssignAgentToTask(invalidId, agentId));

    // Assert
    Assert.Null(ex); // Should quietly fail without throwing
  }

  [Fact]
  public async Task AssignAgentToTask_AssignsSuccessfully_WhenTaskIsUnassigned()
  {
    // Arrange
    var aiService = new FakeAIService();
    var service = new InMemoryTaskService(aiService);
    var task = await service.CreateTaskAsync("Review PR", "Check null guards", Priority.Medium);
    var agentId = "agent-001";

    // Act
    await service.AssignAgentToTask(task.Id, agentId);
    var updated = await service.GetTaskByIdAsync(task.Id);

    // Assert
    Assert.Equal(agentId, updated!.AssignedAgent);
  }

  [Fact]
  public async Task AssignAgentToTask_Throws_WhenAlreadyAssigned()
  {
    // Arrange
    var aiService = new FakeAIService();
    var service = new InMemoryTaskService(aiService);
    var task = await service.CreateTaskAsync("Refactor", "Split methods", Priority.High);
    await service.AssignAgentToTask(task.Id, "agent-001");

    // Act & Assert
    var ex = await Assert.ThrowsAsync<InvalidOperationException>(() =>
        service.AssignAgentToTask(task.Id, "agent-002"));

    Assert.Equal("Task is already assigned.", ex.Message);
  }

  [Fact]
  public async Task AssignAgentToTask_Throws_WhenTaskIsCompleted()
  {
    // Arrange
    var aiService = new FakeAIService();
    var service = new InMemoryTaskService(aiService);
    var task = await service.CreateTaskAsync("Deploy fix", "Production deploy", Priority.High);
    task.MarkAsCompleted(); // simulate external completion

    // Act & Assert
    var ex = await Assert.ThrowsAsync<InvalidOperationException>(() =>
        service.AssignAgentToTask(task.Id, "agent-003"));

    Assert.Equal("Cannot assign a completed task.", ex.Message);
  }

  [Fact]
  public async Task AssignAgentToTask_DoesNothing_WhenTaskNotFound()
  {
    // Arrange
    var aiService = new FakeAIService();
    var service = new InMemoryTaskService(aiService);
    var randomId = Guid.NewGuid();

    // Act
    var ex = await Record.ExceptionAsync(() =>
        service.AssignAgentToTask(randomId, "agent-ghost"));

    // Assert
    Assert.Null(ex);
  }

  [Fact]
  public async Task MarkTaskAsCompleted_UpdatesIsCompletedFlag()
  {
    // Arrange
    var aiService = new FakeAIService();
    var service = new InMemoryTaskService(aiService);
    var task = await service.CreateTaskAsync("Write docs", "Add XML comments", Priority.Low);

    // Act
    await service.MarkTaskAsCompleted(task.Id);
    var updated = await service.GetTaskByIdAsync(task.Id);

    // Assert
    Assert.True(updated!.IsCompleted);
    Assert.NotNull(updated.CompletedAt);
    Assert.True((DateTime.UtcNow - updated.CompletedAt!.Value).TotalSeconds < 5);
  }

  [Fact]
  public async Task MarkTaskAsCompleted_Throws_WhenTaskAlreadyCompleted()
  {
    // Arrange
    var aiService = new FakeAIService();
    var service = new InMemoryTaskService(aiService);
    var task = await service.CreateTaskAsync("Optimize code", "Use Span<T>", Priority.High);
    await service.MarkTaskAsCompleted(task.Id);

    // Act & Assert
    var ex = await Assert.ThrowsAsync<InvalidOperationException>(() =>
        service.MarkTaskAsCompleted(task.Id));

    Assert.Equal("Task is already completed.", ex.Message);
  }

  [Fact]
  public async Task MarkTaskAsCompleted_DoesNothing_WhenTaskNotFound()
  {
    // Arrange
    var aiService = new FakeAIService();
    var service = new InMemoryTaskService(aiService);
    var randomId = Guid.NewGuid();

    // Act
    var ex = await Record.ExceptionAsync(() =>
        service.MarkTaskAsCompleted(randomId));

    // Assert
    Assert.Null(ex);
  }

  [Fact]
  public async Task SummarizeTaskAsync_GeneratesAndStoresSummary()
  {
    // Arrange
    var aiService = new FakeAIService();
    var taskService = new InMemoryTaskService(aiService);
    var task = await taskService.CreateTaskAsync("Debug auth issue", "OAuth2 tokens failing in prod", Priority.High);

    // Act
    await taskService.SummarizeTaskAsync(task.Id);

    // Assert
    var updatedTask = await taskService.GetTaskByIdAsync(task.Id);
    var summary = updatedTask?.Summary;
    Assert.NotNull(summary);
    Assert.Contains("summary", summary.Description, StringComparison.OrdinalIgnoreCase);
  }

}

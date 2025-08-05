namespace Infrastructure.Services;

using Application.DTOs;
using Application.Interfaces;
using Domain.Entities;
using Domain.ValueObjects;

public class InMemoryTaskService : ITaskService
{
  private readonly Dictionary<Guid, TaskItem> tasks = new();

  private readonly IAIService? _aiService;

  public async Task<TaskItem> CreateTaskAsync(CreateTaskRequest dto)
  {
    var priority = dto.Priority ?? await _aiService!.PredictPriorityAsync(dto.Description);

    var task = new TaskItem(dto.Title, dto.Description ?? string.Empty, priority);

    tasks[task.Id] = task;
    return task;
  }

  // manual overload for the method above
  public Task<TaskItem> CreateTaskAsync(string title, string? description, Priority priority)
  {
    var dto = new CreateTaskRequest(title, description ?? "", priority);
    return CreateTaskAsync(dto);
  }


  public Task<TaskItem?> GetTaskByIdAsync(Guid id)
  {
    tasks.TryGetValue(id, out var task);
    return Task.FromResult(task);
  }

  public Task UpdateTaskAsync(Guid id, string? title, string? description, Priority? priority = null)
  {
    if (tasks.TryGetValue(id, out var task))
    {
      task.Update(title, description, priority);
    }

    return Task.CompletedTask;
  }

  public Task AssignAgentToTask(Guid taskId, string agentId)
  {
    if (tasks.TryGetValue(taskId, out var task))
    {
      task.AssignTo(agentId);
    }

    return Task.CompletedTask;
  }

  public Task MarkTaskAsCompleted(Guid taskId)
  {
    if (tasks.TryGetValue(taskId, out var task))
    {
      task.MarkAsCompleted(); // domain rule enforced here
    }

    return Task.CompletedTask;
  }

  public Task<TaskItem[]> GetAllTasksAsync()
  {
    var allTasks = tasks.Values.ToArray();
    return Task.FromResult(allTasks);
  }


  public InMemoryTaskService(IAIService aiService)
  {
    _aiService = aiService;
  }

}

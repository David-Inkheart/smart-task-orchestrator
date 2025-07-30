namespace Infrastructure.Services;

using Application.Interfaces;
using Domain.Entities;
using Domain.ValueObjects;

public class InMemoryTaskService : ITaskService
{
  private readonly Dictionary<Guid, TaskItem> tasks = new();

  public Task<TaskItem> CreateTaskAsync(string title, string? description, Priority priority)
  {
    var task = new TaskItem(title, description ?? string.Empty, priority);
    tasks[task.Id] = task;
    return Task.FromResult(task);
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
}

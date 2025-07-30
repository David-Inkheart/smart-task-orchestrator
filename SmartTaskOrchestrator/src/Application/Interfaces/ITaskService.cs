namespace Application.Interfaces;

using Domain.Entities;
using Domain.ValueObjects;

public interface ITaskService
{
  Task<TaskItem> CreateTaskAsync(string title, string? description, Priority priority);

  Task<TaskItem?> GetTaskByIdAsync(Guid id);

  Task UpdateTaskAsync(Guid id, string? title, string? description, Priority? priority = null);
}

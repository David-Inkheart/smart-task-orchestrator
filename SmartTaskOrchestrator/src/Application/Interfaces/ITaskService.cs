namespace Application.Interfaces;

using Application.DTOs;
using Domain.Entities;
using Domain.ValueObjects;

public interface ITaskService
{
  Task<TaskItem> CreateTaskAsync(CreateTaskRequest dto);

  Task<TaskItem?> GetTaskByIdAsync(Guid id);

  Task UpdateTaskAsync(Guid id, string? title, string? description, Priority? priority = null);

  public Task AssignAgentToTask(Guid taskId, string agentId);

  Task MarkTaskAsCompleted(Guid taskId);
  Task<TaskItem[]> GetAllTasksAsync();
}

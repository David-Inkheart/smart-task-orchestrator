using System.Diagnostics.CodeAnalysis;
using Domain.ValueObjects;

namespace Domain.Entities;
/// <summary>
/// Represents a task item with metadata and priority.
/// </summary>
public class TaskItem
{
  public Guid Id { get; private set; }

  required public string Title { get; set; }

  public string? Description { get; set; }

  public DateTime CreatedAt { get; private set; }

  public Priority Priority { get; private set; }

  public TaskSummary? Summary { get; private set; }
  public bool IsCompleted { get; private set; } = false;
  public string? AssignedAgent { get; private set; }

  public DateTime? UpdatedAt { get; private set; }

  public DateTime? CompletedAt { get; private set; }


  private TaskItem() // for EF Core or deserialization
  {
    Title = string.Empty;
  }

  [SetsRequiredMembers]
  public TaskItem(string title, string description, Priority priority)
  {
    Id = Guid.NewGuid();
    Title = title;
    Description = description;
    CreatedAt = DateTime.UtcNow;
    Priority = priority;
    Summary = new TaskSummary(title, description);
  }

  /// <summary>
  /// Updates the task’s title, description, or priority.
  /// </summary>
  /// <param name="title">The new title for the task. If null or whitespace, the title remains unchanged.</param>
  /// <param name="description">The new description. If null, the description remains unchanged.</param>
  /// <param name="priority">The new priority. If null, the priority remains unchanged.</param>
  public void Update(string? title, string? description, Priority? priority = null)
  {
    bool changed = false;

    if (!string.IsNullOrWhiteSpace(title) && title.Trim() != Title)
    {
      Title = title.Trim();
      changed = true;
    }

    if (description != null && description != Description)
    {
      Description = description;
      changed = true;
    }

    if (priority.HasValue && priority.Value != Priority)
    {
      Priority = priority.Value;
      changed = true;
    }

    if (changed)
      UpdatedAt = DateTime.UtcNow;
  }


  public void MarkAsCompleted()
  {
    if (IsCompleted)
      throw new InvalidOperationException("Task is already completed.");

    IsCompleted = true;
    UpdatedAt = DateTime.UtcNow;
    CompletedAt = DateTime.UtcNow;
  }

  public void AssignTo(string agentId)
  {
    if (IsCompleted)
      throw new InvalidOperationException("Cannot assign a completed task.");

    if (!string.IsNullOrWhiteSpace(AssignedAgent))
      throw new InvalidOperationException("Task is already assigned.");

    AssignedAgent = agentId;
    UpdatedAt = DateTime.UtcNow;
  }


}
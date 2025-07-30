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
    if (!string.IsNullOrWhiteSpace(title))
      Title = title.Trim();

    if (description != null)
      Description = description;

    if (priority.HasValue)
      Priority = priority.Value;
  }
}
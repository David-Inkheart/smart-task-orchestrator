namespace Domain.Entities;

using Domain.ValueObjects;

public class TaskItem
{
  public Guid Id { get; private set; }

  required public string Title { get; set; }

  public string? Description { get; private set; }

  public DateTime CreatedAt { get; private set; }

  public Priority Priority { get; private set; }

  public TaskSummary? Summary { get; private set; }

  private TaskItem() { } // EF Core or deserialization

  public TaskItem(string title, string? description, Priority priority)
  {
    Id = Guid.NewGuid();
    Title = title;
    Description = description;
    CreatedAt = DateTime.UtcNow;
    Priority = priority;
  }

  public void AttachSummary(TaskSummary summary)
  {
    Summary = summary;
  }

  public void Update(string? title, string? description, Priority? priority = null)
  {
    if (!string.IsNullOrWhiteSpace(title))
      Title = title;

    if (description != null)
      Description = description;

    if (priority.HasValue)
      Priority = priority.Value;
  }
}
using Domain.ValueObjects;

namespace Application.DTOs;

public class CreateTaskRequest
{
  public string Title { get; set; }
  public string Description { get; set; }
  public Priority? Priority { get; set; }

  public CreateTaskRequest(string title, string description, Priority? priority = null)
  {
    Title = title;
    Description = description;
    Priority = priority;
  }
}

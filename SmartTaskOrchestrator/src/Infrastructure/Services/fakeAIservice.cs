using Application.Interfaces;
using Domain.ValueObjects;

namespace Infrastructure.Services
{
  public class FakeAIService : IAIService
  {
    public Task<Priority> PredictPriorityAsync(string description)
    {
      // A simple hardcoded logic just for testing
      var priority = description.Contains("urgent", StringComparison.OrdinalIgnoreCase)
          ? Priority.High
          : Priority.Medium;

      return Task.FromResult(priority);
    }
  }
}

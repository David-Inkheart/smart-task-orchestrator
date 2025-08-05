using Domain.ValueObjects;

namespace Application.Interfaces;

public interface IAIService
{
  Task<Priority> PredictPriorityAsync(string description);

  Task<string> GenerateSummaryAsync(string title, string description);
}

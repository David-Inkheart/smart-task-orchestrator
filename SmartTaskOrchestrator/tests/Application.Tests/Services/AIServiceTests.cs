using Application.DTOs;
using Application.Interfaces;
using Domain.Entities;
using Domain.ValueObjects;
using Infrastructure.Services;
using Xunit;

namespace Application.Tests.Services;

public class AIServiceTests
{

  [Fact]
  public async Task Should_AutoAssignPriority_UsingAI_WhenPriorityIsNull()
  {
    // Arrange
    var fakeAIService = new FakeAIService(); // returns Priority.High if contains "urgent"
    var service = new InMemoryTaskService(fakeAIService);

    var dto = new CreateTaskRequest("Fix prod bug", "urgent: API failing", null);

    // Act
    var task = await service.CreateTaskAsync(dto);

    // Assert
    Assert.Equal(Priority.High, task.Priority);
  }

}
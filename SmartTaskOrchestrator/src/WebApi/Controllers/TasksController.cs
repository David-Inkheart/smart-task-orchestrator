using Microsoft.AspNetCore.Mvc;
using Application.Interfaces;
using Application.DTOs;

namespace Controllers;

[ApiController]
[Route("tasks")]
public class TasksController : ControllerBase
{
  private readonly ITaskService _taskService;

  public TasksController(ITaskService taskService)
  {
    _taskService = taskService;
  }

  [HttpPost]
  [Consumes("application/json")]
  public async Task<IActionResult> CreateTask([FromBody] CreateTaskRequest taskPayload)
  {
    var task = await _taskService.CreateTaskAsync(taskPayload);
    return CreatedAtAction(nameof(GetTaskById), new { id = task.Id }, task);
  }

  [HttpGet("{id}")]
  public async Task<IActionResult> GetTaskById(Guid id)
  {
    var task = await _taskService.GetTaskByIdAsync(id);
    if (task == null) return NotFound();
    return Ok(task);
  }

  [HttpGet]
  public async Task<IActionResult> GetAll()
  {
    var tasks = await _taskService.GetAllTasksAsync();
    return Ok(tasks);
  }
}

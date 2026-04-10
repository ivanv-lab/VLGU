using AdvertisingAgency.Contract.Task;
using AdvertisingAgency.Repository;
using Microsoft.AspNetCore.Mvc;
using Task = AdvertisingAgency.Model.Task;

namespace AdvertisingAgency.Controllers;

[ApiController]
[Route("api/tasks")]
[Produces("application/json")]
public class TaskController : ControllerBase
{
    private readonly TaskRepository repository;

    public TaskController(TaskRepository repository)
    {
        this.repository = repository;
    }

    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<Task>),
        StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<Task>>> getAll()
    {
        try
        {
            return Ok(await repository.getAll());
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Internal server error: {ex.Message}");
        }
    }

    [HttpGet("{id}")]
    [ProducesResponseType(typeof(Task),
        StatusCodes.Status404NotFound)]
    public async Task<ActionResult<Task>> get(long id)
    {
        try
        {
            Task task = await repository.get(id);
            if (task == null)
                return NotFound($"Task with id {id} not found");

            return Ok(task);
        }
        catch (InvalidOperationException)
        {
            return NotFound($"Task with id {id} not found");
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Internal server error: {ex.Message}");
        }
    }

    [HttpPost]
    [ProducesResponseType(typeof(Task), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<Task>> create([FromBody] TaskCreateContract taskCreate)
    {
        try
        {
            string error = validateTaskContract(taskCreate);

            if (error == null)
            {
                Task task = new Task(0,
                    taskCreate.title,
                    taskCreate.body,
                    taskCreate.deadline,
                    taskCreate.statusId,
                    taskCreate.assignedUserId,
                    taskCreate.campaignId);
                task = await repository.save(task);

                return CreatedAtAction(nameof(get),
                    new { id = task.id }, task);
            }
            else return BadRequest(error);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Internal server error: {ex.Message}");
        }
    }

    [HttpPut("{id}")]
    [ProducesResponseType(typeof(Task), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<Task>> update(long id,
        [FromBody] TaskCreateContract taskUpdate)
    {
        try
        {
            string error = validateTaskContract(taskUpdate);

            if (error == null)
            {
                Task updatedStatus = await repository
                    .update(id, new Task(id,
                        taskUpdate.title,
                        taskUpdate.body,
                        taskUpdate.deadline,
                        taskUpdate.statusId,
                        taskUpdate.assignedUserId,
                        taskUpdate.campaignId));

                if (updatedStatus == null)
                    return NotFound($"Task with id {id} not found");

                return Ok(updatedStatus);
            }
            else return BadRequest(error);
        }
        catch (InvalidOperationException)
        {
            return NotFound($"Task with id {id} not found");
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Internal server error: {ex.Message}");
        }
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<bool>> delete(long id)
    {
        try
        {
            bool result = await repository.delete(id);
            if (result)
                return NoContent();
            else return NotFound($"Task with id {id} not found");
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Internal server error: {ex.Message}");
        }
    }

    private string validateTaskContract(TaskCreateContract contract)
    {
        if (contract == null)
            return "Task data is null";

        if (string.IsNullOrEmpty(contract.title))
            return "Task title is required";

        if (string.IsNullOrEmpty(contract.body))
            return "Task body is required";

        if (string.IsNullOrEmpty(contract.deadline.ToString()))
            return "Task deadline is required";

        if (contract.assignedUserId == null)
            return "Task assigned user is required";

        if (contract.campaignId == null)
            return "Task campaign is required";

        if (contract.statusId == null)
            return "Task status is required";

        return null;
    }
}
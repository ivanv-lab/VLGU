using AdvertisingAgency.Contract.Task;
using AdvertisingAgency.Repository;
using Microsoft.AspNetCore.Authorization;
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
    [Authorize(Roles = "Manager")]
    [ProducesResponseType(typeof(IEnumerable<TaskGetContract>),
        StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<TaskGetContract>>> getAll()
    {
        try
        {
            IEnumerable<Task> tasks = await repository.getAll();
            return Ok(tasks.Select(t=>new TaskGetContract(
                t.id,
                t.title,
                t.body,
                t.deadline,
                t.taskStatus.name,
                t.assignedUser.UserName,
                t.campaign==null?null:t.campaign.name
                )));
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Internal server error: {ex.Message}");
        }
    }

    [HttpGet("{id}")]
    [Authorize(Roles = "Manager,Guest")]
    [ProducesResponseType(typeof(TaskGetContract),
        StatusCodes.Status404NotFound)]
    public async Task<ActionResult<TaskGetContract>> get(long id)
    {
        try
        {
            Task task = await repository.get(id);
            if (task == null)
                return NotFound($"Task with id {id} not found");

            return Ok(new TaskGetContract(task.id, task.title,
                task.body,task.deadline,task.taskStatus.name,
                task.assignedUser.UserName,task.campaign.name));
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
    [Authorize(Roles = "Manager")]
    [ProducesResponseType(typeof(TaskGetContract), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<TaskGetContract>> create([FromBody] TaskCreateContract taskCreate)
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

                return Ok(new TaskGetContract(
                    task.id,
                    task.title,
                    task.body,
                    task.deadline,
                    task.taskStatus.name,
                    task.assignedUser.UserName,
                    task.campaign.name
                    ));
            }
            else return BadRequest(error);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Internal server error: {ex.Message}");
        }
    }

    [HttpPut("{id}")]
    [Authorize(Roles = "Manager")]
    [ProducesResponseType(typeof(TaskGetContract), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<TaskGetContract>> update(long id,
        [FromBody] TaskCreateContract taskUpdate)
    {
        try
        {
            string error = validateTaskContract(taskUpdate);

            if (error == null)
            {
                Task updatedTask = await repository
                    .update(id, new Task(id,
                        taskUpdate.title,
                        taskUpdate.body,
                        taskUpdate.deadline,
                        taskUpdate.statusId,
                        taskUpdate.assignedUserId,
                        taskUpdate.campaignId));

                if (updatedTask == null)
                    return NotFound($"Task with id {id} not found");

                return Ok(new TaskGetContract(
                    updatedTask.id,
                    updatedTask.title,
                    updatedTask.body,
                    updatedTask.deadline,
                    updatedTask.taskStatus.name,
                    updatedTask.assignedUser.UserName,
                    updatedTask.campaign.name
                    ));
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
    [Authorize(Roles = "Manager")]
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
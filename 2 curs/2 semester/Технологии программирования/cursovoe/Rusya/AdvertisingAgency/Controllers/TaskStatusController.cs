using AdvertisingAgency.Contract.TaskStatus;
using AdvertisingAgency.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TaskStatus = AdvertisingAgency.Model.TaskStatus;

namespace AdvertisingAgency.Controllers
{
    [ApiController]
    [Route("api/task/statuses")]
    [Produces("application/json")]
    public class TaskStatusController : ControllerBase
    {
        private readonly TaskStatusRepository repository;

        public TaskStatusController(TaskStatusRepository repository)
        {
            this.repository = repository;
        }

        [HttpGet]
        [Authorize(Roles = "Admin,Manager")]
        [ProducesResponseType(typeof(IEnumerable<TaskStatus>),
            StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<TaskStatus>>> getAll()
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
        [Authorize(Roles = "Admin,Manager")]
        [ProducesResponseType(typeof(TaskStatus),
            StatusCodes.Status404NotFound)]
        public async Task<ActionResult<TaskStatus>> get(int id)
        {
            try
            {
                TaskStatus taskStatus = await repository.get(id);
                if (taskStatus == null)
                    return NotFound($"Task status with id {id} not found");

                return Ok(taskStatus);
            }
            catch (InvalidOperationException)
            {
                return NotFound($"Task status with id {id} not found");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        [ProducesResponseType(typeof(TaskStatus),
            StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<TaskStatus>> create([FromBody] TaskStatusCreateContract taskStatusCreate)
        {
            try
            {
                string error = await validateTaskStatusContract(taskStatusCreate);

                if (error == null)
                {
                    TaskStatus taskStatus = new TaskStatus(0, taskStatusCreate.name);
                    taskStatus = await repository.save(taskStatus);

                    return Ok(taskStatus);
                }
                else return BadRequest(error);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        [ProducesResponseType(typeof(TaskStatus), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<TaskStatus>> update(int id, [FromBody] TaskStatusCreateContract taskStatusUpdate)
        {
            try
            {
                string error = await validateTaskStatusContract(taskStatusUpdate);

                if (error == null)
                {
                    TaskStatus updatedTaskStatus = await repository
                        .update(id, new TaskStatus(id, taskStatusUpdate.name));

                    if (updatedTaskStatus == null)
                        return NotFound($"Task status with id {id} not found");

                    return Ok(updatedTaskStatus);
                }
                else return BadRequest(error);
            }
            catch (InvalidOperationException)
            {
                return NotFound($"Task status with id {id} not found");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<bool>> delete(int id)
        {
            try
            {
                bool result = await repository.delete(id);
                if (result)
                    return Ok();
                else
                    return NotFound($"Task status with id {id} not found");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        private async Task<string> validateTaskStatusContract(TaskStatusCreateContract contract)
        {
            if (contract == null)
                return "Task status data is null";

            if (string.IsNullOrEmpty(contract.name) || string.IsNullOrWhiteSpace(contract.name))
                return "Task status name is required";

            if (await repository.isTaskStatusExists(contract.name))
                return "This status is already exists";

            return null;
        }
    }
}

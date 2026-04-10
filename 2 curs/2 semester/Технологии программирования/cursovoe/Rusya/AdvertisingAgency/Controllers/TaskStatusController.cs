using AdvertisingAgency.Contract.TaskStatus;
using AdvertisingAgency.Repository;
using Microsoft.AspNetCore.Mvc;
using TaskStatus = AdvertisingAgency.Model.TaskStatus;

namespace AdvertisingAgency.Controllers
{
    [ApiController]
    [Route("api/task/statuses")]
    [Produces("application/json")]
    public class TaskStatusController: ControllerBase
    {
        private readonly TaskStatusRepository repository;

        public TaskStatusController(TaskStatusRepository repository)
        {
            this.repository = repository;
        }

        [HttpGet]
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
        [ProducesResponseType(typeof(TaskStatus),
            StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<TaskStatus>> create([FromBody] TaskStatusCreateContract taskStatusCreate)
        {
            try
            {
                if (taskStatusCreate == null)
                    return BadRequest("Task status data is null");

                if (string.IsNullOrEmpty(taskStatusCreate.name))
                    return BadRequest("Task status name is required");

                TaskStatus taskStatus = new TaskStatus(0, taskStatusCreate.name);
                taskStatus = await repository.save(taskStatus);

                return CreatedAtAction(nameof(get),
                    new { id = taskStatus.id }, taskStatus);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpPut("{id}")]
        [ProducesResponseType(typeof(TaskStatus), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<TaskStatus>> update(int id, [FromBody] TaskStatusCreateContract taskStatusUpdate)
        {
            try
            {
                if (taskStatusUpdate == null)
                    return BadRequest("Task status data is null");

                if (string.IsNullOrEmpty(taskStatusUpdate.name))
                    return BadRequest("Task status name is required");

                TaskStatus updatedTaskStatus = await repository
                    .update(id, new TaskStatus(id, taskStatusUpdate.name));

                if (updatedTaskStatus == null)
                    return NotFound($"Task status with id {id} not found");

                return Ok(updatedTaskStatus);
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
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<bool>> delete(int id)
        {
            try
            {
                bool result = await repository.delete(id);
                if (result)
                    return NoContent();
                else
                    return NotFound($"Task status with id {id} not found");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}

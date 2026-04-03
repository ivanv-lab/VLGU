using Lab4.Contract.Additional.ApplicationStatus;
using Lab4.Model.Additional;
using Lab4.Repository.Additional;
using log4net;
using Microsoft.AspNetCore.Mvc;

namespace Lab4.Controllers
{
    [ApiController]
    [Route("api/application_status")]
    [Produces("application/json")]
    public class ApplicationStatusController : ControllerBase
    {
        private readonly ApplicationStatusRepository
            _repository;
        private readonly ILog _log=LogManager
            .GetLogger(typeof(ApplicationStatusController));

        public ApplicationStatusController(ApplicationStatusRepository repository)
        {
            _repository = repository;
        }

        // GET: api/application_status
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<ApplicationStatus>), StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<ApplicationStatus>>> getAll()
        {
            _log.Debug("Routing GET:api/application_status");
            try
            {
                return Ok(await _repository.getAll());
            }
            catch (Exception ex)
            {
                _log.Error(ex);
                return StatusCode(500,
                    $"Internal server error: {ex.Message}");
            }
        }

        // GET: api/application_status/5
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(ApplicationStatus), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ApplicationStatus>> get(long id)
        {
            _log.Debug($"Routing GET:api/application_status/{id}");
            try
            {
                var status = await _repository.get(id);

                if (status == null)
                {
                    return NotFound($"Application status with id {id} not found");
                }

                return Ok(status);
            }
            catch (InvalidOperationException)
            {
                return NotFound($"Application status with id {id} not found");
            }
            catch (Exception ex)
            {
                _log.Error(ex);
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // POST: api/application_status
        [HttpPost]
        [ProducesResponseType(typeof(ApplicationStatus), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<ApplicationStatus>> create([FromBody] ApplicationStatusCreateContract createContract)
        {
            _log.Debug($"Routing POST:api/application_status" +
                $"name: {createContract.name}");
            try
            {
                if (createContract == null)
                {
                    return BadRequest("Application status data is null");
                }

                if (string.IsNullOrWhiteSpace(createContract.name))
                {
                    return BadRequest("Application status name is required");
                }

                ApplicationStatus applicationStatus = new ApplicationStatus(0, createContract.name);

                ApplicationStatus createdStatus = await _repository.save(applicationStatus);

                return CreatedAtAction(nameof(get), new { id = createdStatus.id }, createdStatus);
            }
            catch (Exception ex)
            {
                _log.Error(ex);
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // PUT: api/application_status/5
        [HttpPut("{id}")]
        [ProducesResponseType(typeof(ApplicationStatus), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> update(long id, [FromBody] ApplicationStatusCreateContract updateContract)
        {
            _log.Debug($"Routing PUT:api/application_status/{id}" +
                $"name: {updateContract.name}");
            try
            {
                if (updateContract == null)
                {
                    return BadRequest("Application status data is null");
                }

                if (string.IsNullOrWhiteSpace(updateContract.name))
                {
                    return BadRequest("Application status name is required");
                }

                ApplicationStatus applicationStatus = new ApplicationStatus(id, updateContract.name);

                var updatedStatus = await _repository.update(id, applicationStatus);
                _log.Debug("Founded updated 'ApplicationStatus':" +
                    $"[id: {updatedStatus.id}, name: {updatedStatus.name}]");

                if (updatedStatus == null)
                {
                    return NotFound($"Application status with id {id} not found");
                }

                return Ok(updatedStatus);
            }
            catch (InvalidOperationException)
            {
                return NotFound($"Application status with id {id} not found");
            }
            catch (Exception ex)
            {
                _log.Error(ex);
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // DELETE: api/application_status/5
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> delete(long id)
        {
            _log.Debug($"Routing DELETE:api/application_status/{id}");
            try
            {
                var result = await _repository.delete(id);

                if (result)
                {
                    return NoContent();
                }
                else
                {
                    return NotFound($"Application status with id {id} not found");
                }
            }
            catch (Exception ex)
            {
                _log.Error(ex);
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // GET: api/application_status/search?name=value
        [HttpGet("search")]
        [ProducesResponseType(typeof(IEnumerable<ApplicationStatus>), StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<ApplicationStatus>>> search([FromQuery] string name)
        {
            _log.Debug($"Routing GET:api/application_status/search?name={name}");
            try
            {
                List<ApplicationStatus> statuses = await _repository.getAll();

                if (!string.IsNullOrWhiteSpace(name))
                {
                    statuses = statuses
                        .Where(s => s.name
                        .Contains(name, StringComparison
                        .OrdinalIgnoreCase))
                        .ToList();
                }

                return Ok(statuses);
            }
            catch (Exception ex)
            {
                _log.Error(ex);
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}

using AdvertisingAgency.Contract.Role;
using AdvertisingAgency.Model.Authorization;
using AdvertisingAgency.Repository.Authorization;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AdvertisingAgency.Controllers
{
    [ApiController]
    [Route("api/roles")]
    [Produces("application/json")]
    public class RoleController:ControllerBase
    {
        private readonly RoleRepository repository;

        public RoleController(RoleRepository repository)
        {
            this.repository = repository;
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        [ProducesResponseType(typeof(IEnumerable<Role>),
            StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<Role>>> getAll()
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
        [Authorize(Roles = "Admin")]
        [ProducesResponseType(typeof(Role),
            StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Role>> get(int id)
        {
            try
            {
                Role role = await repository.get(id);
                if (role == null)
                    return NotFound($"Role with id {id} not found");

                return Ok(role);
            }
            catch(InvalidOperationException)
            {
                return NotFound($"Role with id {id} not found");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        [ProducesResponseType(typeof(Role),StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Role>> create([FromBody] RoleCreateContract roleCreate)
        {
            try
            {
                if (roleCreate == null)
                    return BadRequest("Role data is null");

                if (string.IsNullOrEmpty(roleCreate.name))
                    return BadRequest("Role name is required");

                Role role = new Role(0, roleCreate.name);
                role = await repository.save(role);

                return CreatedAtAction(nameof(get),
                    new { id = role.id }, role);
            } catch(Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        [ProducesResponseType(typeof(Role), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Role>> update(int id, [FromBody]
        RoleCreateContract roleUpdate)
        {
            try
            {
                if (roleUpdate == null)
                    return BadRequest("Role data is null");

                if (string.IsNullOrEmpty(roleUpdate.name))
                    return BadRequest("Role name is reqiered");

                Role updatedRole = await repository
                    .update(id, new Role(id, roleUpdate.name));

                if (updatedRole == null)
                    return NotFound($"Role with id {id} not found");

                return Ok(updatedRole);
            }
            catch (InvalidOperationException)
            {
                return NotFound($"Role with id {id} not found");
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
                    return NoContent();
                else
                    return NotFound($"Role with id {id} not found");
            } catch(Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}

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
        [ProducesResponseType(typeof(IEnumerable<RoleGetContract>),
            StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<RoleGetContract>>> getAll()
        {
            try
            {
                IEnumerable<Role> roles = await repository.getAll();
                return Ok(roles.Select(r => new RoleGetContract(
                    r.Id,
                    r.Name
                    )));
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpGet("{id}")]
        [Authorize(Roles = "Admin")]
        [ProducesResponseType(typeof(RoleGetContract),
            StatusCodes.Status404NotFound)]
        public async Task<ActionResult<RoleGetContract>> get(string id)
        {
            try
            {
                Role role = await repository.get(id);
                if (role == null)
                    return NotFound($"Role with id {id} not found");

                return Ok(new RoleGetContract(role.Id, role.Name));
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

                Role role = new Role(roleCreate.name);
                role = await repository.save(role);

                return CreatedAtAction(nameof(get),
                    new { id = role.Id }, role);
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
        public async Task<ActionResult<Role>> update(string id, [FromBody]
        RoleCreateContract roleUpdate)
        {
            try
            {
                if (roleUpdate == null)
                    return BadRequest("Role data is null");

                if (string.IsNullOrEmpty(roleUpdate.name))
                    return BadRequest("Role name is reqiered");

                Role updatedRole = await repository
                    .update(id, new Role(roleUpdate.name));

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
        public async Task<ActionResult<bool>> delete(string id)
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

using AdvertisingAgency.Contract.User;
using AdvertisingAgency.Model.Authorization;
using AdvertisingAgency.Repository.Authorization;
using AdvertisingAgency.Service;
using Microsoft.AspNetCore.Mvc;

namespace AdvertisingAgency.Controllers;

[ApiController]
[Route("api/users")]
[Produces("application/json")]
public class UserController : ControllerBase
{
    private readonly UserRepository repository;
    private readonly AuthService authService;

    public UserController(UserRepository repository,
        AuthService authService)
    {
        this.repository = repository;
        this.authService = authService;
    }

    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<User>),
        StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<User>>> getAll()
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
    [ProducesResponseType(typeof(User),
        StatusCodes.Status404NotFound)]
    public async Task<ActionResult<User>> get(long id)
    {
        try
        {
            User user = await repository.get(id);
            if (user == null)
                return NotFound($"User with id {id} not found");

            return Ok(user);
        }
        catch (InvalidOperationException)
        {
            return NotFound($"User with id {id} not found");
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Internal server error: {ex.Message}");
        }
    }

    [HttpPost]
    [ProducesResponseType(typeof(User), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<User>> create([FromBody] UserCreateContract userCreate)
    {
        try
        {
            if (userCreate == null)
                return BadRequest("User data is null");

            if (string.IsNullOrEmpty(userCreate.fullname))
                return BadRequest("User fullname is required");

            if (string.IsNullOrEmpty(userCreate.email))
                return BadRequest("User email is required");

            if (string.IsNullOrEmpty(userCreate.password))
                return BadRequest("User password is required");

            if (userCreate.roleId == null || userCreate.roleId == 0)
                return BadRequest("User role id is required");

            User user = new User(0, userCreate.fullname,
                userCreate.email, 
                authService.hashPassword(userCreate.password),
                userCreate.roleId);
            user = await repository.save(user);

            return CreatedAtAction(nameof(get),
                new { id = user.id }, user);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Internal server error: {ex.Message}");
        }
    }

    [HttpPut("{id}")]
    [ProducesResponseType(typeof(User), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<User>> update(long id, [FromBody] UserCreateContract userUpdate)
    {
        try
        {
            if (userUpdate == null)
                return BadRequest("User data is null");

            if (string.IsNullOrEmpty(userUpdate.fullname))
                return BadRequest("User fullname is required");

            if (string.IsNullOrEmpty(userUpdate.email))
                return BadRequest("User email is required");

            if (string.IsNullOrEmpty(userUpdate.password))
                return BadRequest("User password is required");

            if (userUpdate.roleId == null || userUpdate.roleId == 0)
                return BadRequest("User role id is required");

            User updatedUser = await repository
                .update(id, new User(id, userUpdate.fullname,
                    userUpdate.email, 
                    authService.hashPassword(userUpdate.password),
                    userUpdate.roleId));

            if (updatedUser == null)
                return NotFound($"User with id {id} not found");

            return Ok(updatedUser);
        }
        catch (InvalidOperationException)
        {
            return NotFound($"User with id {id} not found");
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
            else
                return NotFound($"User with id {id} not found");
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Internal server error: {ex.Message}");
        }
    }
}
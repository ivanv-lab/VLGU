using AdvertisingAgency.Contract.User;
using AdvertisingAgency.Model.Authorization;
using AdvertisingAgency.Repository.Authorization;
using AdvertisingAgency.Service;
using Microsoft.AspNetCore.Authorization;
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
    [Authorize(Roles = "Admin,Manager,Guest")]
    [ProducesResponseType(typeof(IEnumerable<UserGetContract>),
        StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<UserGetContract>>> getAll()
    {
        try
        {
            IEnumerable<User> users = await repository.getAll();
            IEnumerable<UserGetContract> userGetContracts = users.Select(user => new UserGetContract(
                user.Id,
                user.UserName,
                user.Email,
                user.roleId,
                user.role.Name,
                user.client?.id,
                user.client?.name
                ));
            return Ok(userGetContracts);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Internal server error: {ex.Message}");
        }
    }

    [HttpGet("{id}")]
    [Authorize(Roles = "Admin,Manager")]
    [ProducesResponseType(typeof(UserGetContract),
        StatusCodes.Status404NotFound)]
    public async Task<ActionResult<UserGetContract>> get(string id)
    {
        try
        {
            User user = await repository.get(id);
            if (user == null)
                return NotFound($"User with id {id} not found");

            return Ok(new UserGetContract(
                user.Id,
                user.UserName,
                user.Email,
                user.roleId,
                user.role.Name,
                user.client?.id,
                user.client?.name
                ));
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
    [Authorize(Roles = "Admin")]
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

            if (userCreate.roleId == null || userCreate.roleId.IsWhiteSpace())
                return BadRequest("User role id is required");

            User user = new User
                (userCreate.fullname,
                userCreate.email,
                authService.hashPassword(null, userCreate.password),
                userCreate.roleId,
                userCreate.clientId);
            user = await repository.save(user);

            return CreatedAtAction(nameof(get),
                new { id = user.Id }, user);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Internal server error: {ex.Message}");
        }
    }

    [HttpPut("{id}")]
    [Authorize(Roles = "Admin")]
    [ProducesResponseType(typeof(User), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<User>> update(string id, [FromBody] UserCreateContract userUpdate)
    {
        try
        {
            if (userUpdate == null)
                return BadRequest("User data is null");

            if (string.IsNullOrEmpty(userUpdate.fullname))
                return BadRequest("User fullname is required");

            if (userUpdate.roleId == null || userUpdate.roleId.IsWhiteSpace())
                return BadRequest("User role id is required");

            User updatedUser = await repository
                .update(id, new User(
                userUpdate.fullname,
                userUpdate.email,
                authService.hashPassword(null, userUpdate.password),
                userUpdate.roleId,
                userUpdate.clientId));

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
                return NotFound($"User with id {id} not found");
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Internal server error: {ex.Message}");
        }
    }
}
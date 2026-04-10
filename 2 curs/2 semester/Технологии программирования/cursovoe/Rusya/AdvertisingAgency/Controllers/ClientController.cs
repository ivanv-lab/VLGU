using AdvertisingAgency.Contract.Client;
using AdvertisingAgency.Model;
using AdvertisingAgency.Repository;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace AdvertisingAgency.Controllers;

[ApiController]
[Route("api/clients")]
[Produces("application/json")]
public class ClientController:ControllerBase
{
    private readonly ClientRepository repository;

    public ClientController(ClientRepository repository)
    {
        this.repository = repository;
    }

    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<Client>),
        StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<Client>>> getAll()
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
    [ProducesResponseType(typeof(Client),
        StatusCodes.Status404NotFound)]
    public async Task<ActionResult<Client>> get(long id)
    {
        try
        {
            Client client = await repository.get(id);
            if (client == null)
                return NotFound($"Client with id {id} not found");

            return Ok(client);
        }
        catch (InvalidOperationException)
        {
            return NotFound($"Client with id {id} not found");
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Internal server error: {ex.Message}");
        }
    }

    [HttpPost]
    [ProducesResponseType(typeof(Client),
        StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<Client>> create([FromBody] ClientCreateContract clientCreate)
    {
        try
        {
            string error = validateClientContract(clientCreate);

            if (error == null)
            {
                Client client = new Client(0,
                    clientCreate.name,
                    clientCreate.contactPersonFullname,
                    clientCreate.email,
                    clientCreate.phone,
                    clientCreate.address);
                client = await repository.save(client);

                return CreatedAtAction(nameof(get),
                    new { id = client.id }, client);
            }
            else return BadRequest(error);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Internal server error: {ex.Message}");
        }
    }

    [HttpPut("{id}")]
    [ProducesResponseType(typeof(Client), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<Client>> update(long id,
        [FromBody] ClientCreateContract clientUpdate)
    {
        try
        {
            string error = validateClientContract(clientUpdate);

            if (error == null)
            {
                Client updatedClient = await repository
                    .update(id, new Client(id,
                        clientUpdate.name,
                        clientUpdate.contactPersonFullname,
                        clientUpdate.email,
                        clientUpdate.phone,
                        clientUpdate.address));

                if (updatedClient == null)
                    return NotFound($"Client with id {id} not found");

                return Ok(updatedClient);
            }
            else return BadRequest(error);
        }
        catch (InvalidOperationException)
        {
            return NotFound($"Client with id {id} not found");
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
            else return NotFound($"Client with id {id} not found");
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Internal server error: {ex.Message}");
        }
    }

    private string validateClientContract(ClientCreateContract contract)
    {
        if (contract == null)
            return "Client data is null";

        if (string.IsNullOrEmpty(contract.name))
            return "Client name is required";

        if (string.IsNullOrEmpty(contract.address))
            return "Client address is required";

        if (string.IsNullOrEmpty(contract.contactPersonFullname))
            return "Client contact name is required";

        if (string.IsNullOrEmpty(contract.email))
            return "Client email is required";

        if (string.IsNullOrEmpty(contract.phone))
            return "Client phone is required";

        return null;
    }
}
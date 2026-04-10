using AdvertisingAgency.Contract.CampaignStatus;
using AdvertisingAgency.Model;
using AdvertisingAgency.Repository;
using Microsoft.AspNetCore.Mvc;
using TaskStatus = AdvertisingAgency.Model.TaskStatus;

namespace AdvertisingAgency.Controllers;

[ApiController]
[Route("api/campaign/statuses")]
[Produces("application/json")]
public class CampaignStatusController:ControllerBase
{
    private readonly CampaignStatusRepository repository;

    public CampaignStatusController(CampaignStatusRepository repository)
    {
        this.repository = repository;
    }

    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<CampaignStatus>),
        StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<CampaignStatus>>> getAll()
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
    [ProducesResponseType(typeof(CampaignStatus),
        StatusCodes.Status404NotFound)]
    public async Task<ActionResult<CampaignStatus>> get(int id)
    {
        try
        {
            CampaignStatus campaignStatus = await repository.get(id);
            if (campaignStatus == null)
                return NotFound($"Campaign status with id {id} not found");

            return Ok(campaignStatus);
        }
        catch (InvalidOperationException)
        {
            return NotFound($"Campaign status with id {id} not found");
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Internal server error: {ex.Message}");
        }
    }

    [HttpPost]
    [ProducesResponseType(typeof(CampaignStatus), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<CampaignStatus>> create([FromBody] CampaignStatusCreateContract statusCreate)
    {
        try
        {
            if (statusCreate == null)
                return BadRequest("Campaign status data is null");

            if (string.IsNullOrEmpty(statusCreate.name))
                return BadRequest("Campaign status name is required");

            CampaignStatus campaignStatus = new CampaignStatus(0, statusCreate.name);
            await repository.save(campaignStatus);

            return CreatedAtAction(nameof(get),
                new { id = campaignStatus.id }, campaignStatus);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Internal server error: {ex.Message}");
        }
    }

    [HttpPut("{id}")]
    [ProducesResponseType(typeof(CampaignStatus), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<CampaignStatus>> update(int id,
        [FromBody] CampaignStatusCreateContract statusUpdate)
    {
        try
        {
            if (statusUpdate == null)
                return BadRequest("Campaign status data is null");

            if (string.IsNullOrEmpty(statusUpdate.name))
                return BadRequest("Campaign status name is required");

            CampaignStatus updatedStatus = await repository
                .update(id, new CampaignStatus(id, statusUpdate.name));

            if (updatedStatus == null)
                return NotFound($"Campaign status with id {id} not found");

            return Ok(updatedStatus);
        }
        catch (InvalidOperationException)
        {
            return NotFound($"Campaign status with id {id} not found");
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
            if (result) return NoContent();
            else return NotFound($"Campaign status with id {id} not found");
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Internal server error: {ex.Message}");
        }
    } 
}
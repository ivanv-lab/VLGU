using AdvertisingAgency.Contract.CampaignStatus;
using AdvertisingAgency.Model;
using AdvertisingAgency.Repository;
using Microsoft.AspNetCore.Authorization;
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
    [Authorize(Roles = "Admin,Manager")]
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
    [Authorize(Roles = "Admin,Manager")]
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
    [Authorize(Roles = "Admin")]
    [ProducesResponseType(typeof(CampaignStatus), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<CampaignStatus>> create([FromBody] CampaignStatusCreateContract statusCreate)
    {
        try
        {
            string error = await validateCampaignStatusContract(statusCreate);

            if (error == null)
            {
                CampaignStatus campaignStatus = new CampaignStatus(0, statusCreate.name);
                await repository.save(campaignStatus);

                return Ok(campaignStatus);
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
    [ProducesResponseType(typeof(CampaignStatus), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<CampaignStatus>> update(int id,
        [FromBody] CampaignStatusCreateContract statusUpdate)
    {
        try
        {
            string error = await validateCampaignStatusContract(statusUpdate);

            if (error == null)
            {
                CampaignStatus updatedStatus = await repository
                    .update(id, new CampaignStatus(id, statusUpdate.name));

                if (updatedStatus == null)
                    return NotFound($"Campaign status with id {id} not found");

                return Ok(updatedStatus);
            }
            else return BadRequest(error);
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
    [Authorize(Roles = "Admin")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<bool>> delete(int id)
    {
        try
        {
            bool result = await repository.delete(id);
            if (result) return Ok();
            else return NotFound($"Campaign status with id {id} not found");
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Internal server error: {ex.Message}");
        }
    } 

    private async Task<string> validateCampaignStatusContract(CampaignStatusCreateContract contract)
    {
        if (contract == null)
            return "Campaign status data is null";

        if (string.IsNullOrEmpty(contract.name) || string.IsNullOrWhiteSpace(contract.name))
            return "Campaign status name is required";

        if (await repository.isStatusExists(contract.name))
            return "This status is already exists";

        return null;
    }
}
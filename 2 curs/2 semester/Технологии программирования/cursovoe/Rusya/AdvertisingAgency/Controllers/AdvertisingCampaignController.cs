using AdvertisingAgency.Contract.AdvertisingCampaign;
using AdvertisingAgency.Model;
using AdvertisingAgency.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Task = System.Threading.Tasks.Task;

namespace AdvertisingAgency.Controllers;

[ApiController]
[Route("api/campaigns")]
[Produces("application/json")]
public class AdvertisingCampaignController : ControllerBase
{
    private readonly AdvertisingCampaignRepository repository;

    public AdvertisingCampaignController(AdvertisingCampaignRepository repository)
    {
        this.repository = repository;
    }

    [HttpGet]
    [Authorize(Roles = "Manager,Admin,Guest")]
    [ProducesResponseType(typeof(IEnumerable<AdvertisingCampaignGetContract>),
        StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<AdvertisingCampaignGetContract>>> getAll()
    {
        try
        {
            IEnumerable<AdvertisingCampaign> advertisingCampaigns = await repository.getAll();
            return Ok(advertisingCampaigns.Select(ac=>new AdvertisingCampaignGetContract(
                ac.id,
                ac.name,
                ac.description,
                ac.startDate,
                ac.endDate,
                ac.budget,
                ac.status.name,
                ac.category.name,
                ac.client.name,
                ac.tasks.Select(t=>t.title).ToList()
                )));
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Internal server error: {ex.Message}");
        }
    }

    [HttpGet("{id}")]
    [Authorize(Roles = "Manager,Guest,Admin")]
    [ProducesResponseType(typeof(AdvertisingCampaignGetContract),
        StatusCodes.Status404NotFound)]
    public async Task<ActionResult<AdvertisingCampaignGetContract>> get(long id)
    {
        try
        {
            AdvertisingCampaign advertisingCampaign = await
                repository.get(id);
            if (advertisingCampaign == null)
                return NotFound($"Advertising campaign with id {id} not found");

            return Ok(new AdvertisingCampaignGetContract(
                advertisingCampaign.id,
                advertisingCampaign.name,
                advertisingCampaign.description,
                advertisingCampaign.startDate,
                advertisingCampaign.endDate,
                advertisingCampaign.budget,
                advertisingCampaign.status.name,
                advertisingCampaign.category.name,
                advertisingCampaign.client.name,
                advertisingCampaign.tasks.Select(t => t.title).ToList()
                ));
        }
        catch (InvalidOperationException)
        {
            return NotFound($"Advertising campaign with id {id} not found");
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Internal server error: {ex.Message}");
        }
    }

    [HttpPost]
    [Authorize(Roles = "Manager")]
    [ProducesResponseType(typeof(AdvertisingCampaignGetContract), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<AdvertisingCampaignGetContract>> create(
        [FromBody] AdvertisingCampaignCreateContract campaignCreate)
    {
        try
        {
            string error = validateAdvertisingCampaignContract(campaignCreate);

            if (error == null)
            {
                AdvertisingCampaign campaign = new AdvertisingCampaign(0,
                    campaignCreate.name,
                    campaignCreate.description,
                    campaignCreate.startDate,
                    campaignCreate.endDate,
                    campaignCreate.budget,
                    campaignCreate.statusId,
                    campaignCreate.categoryId,
                    campaignCreate.clientId);
                campaign = await repository.save(campaign);

                return Ok(new AdvertisingCampaignGetContract(
                    campaign.id,
                    campaign.name,
                    campaign.description,
                    campaign.startDate,
                    campaign.endDate,
                    campaign.budget,
                    campaign.status.name,
                    campaign.category.name,
                    campaign.client.name,
                    campaign.tasks.Select(t => t.title).ToList()
                    ));
            }
            else return BadRequest(error);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Internal server error: {ex.Message}");
        }
    }

    [HttpPut("{id}")]
    [Authorize(Roles = "Manager")]
    [ProducesResponseType(typeof(AdvertisingCampaignGetContract), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<AdvertisingCampaignGetContract>> update(long id,
        [FromBody] AdvertisingCampaignCreateContract campaignUpdate)
    {
        try
        {
            string error = validateAdvertisingCampaignContract(campaignUpdate);

            if (error == null)
            {
                AdvertisingCampaign updatedCampaign = await repository
                    .update(id, new AdvertisingCampaign(id,
                        campaignUpdate.name,
                        campaignUpdate.description,
                        campaignUpdate.startDate,
                        campaignUpdate.endDate,
                        campaignUpdate.budget,
                        campaignUpdate.statusId,
                        campaignUpdate.categoryId,
                        campaignUpdate.clientId));

                if (updatedCampaign == null)
                    return NotFound($"Advertising campaign with id {id} not found");

                return Ok(new AdvertisingCampaignGetContract(
                    updatedCampaign.id,
                    updatedCampaign.name,
                    updatedCampaign.description,
                    updatedCampaign.startDate,
                    updatedCampaign.endDate,
                    updatedCampaign.budget,
                    updatedCampaign.status.name,
                    updatedCampaign.category.name,
                    updatedCampaign.client.name,
                    updatedCampaign.tasks.Select(t=>t.title).ToList()
                    ));
            }
            else return BadRequest(error);
        }
        catch (InvalidOperationException)
        {
            return NotFound($"Advertising campaign with id {id} not found");
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Internal server error: {ex.Message}");
        }
    }

    [HttpDelete("{id}")]
    [Authorize(Roles = "Manager")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<bool>> delete(long id)
    {
        try
        {
            bool result = await repository.delete(id);
            if (result) return NoContent();
            else return NotFound($"Advertising campaign with id {id} not found");
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Internal server error: {ex.Message}");
        }
    }

    private string validateAdvertisingCampaignContract(AdvertisingCampaignCreateContract contract)
    {
        if (contract == null)
            return "Advertising campaign data is null";

        if (string.IsNullOrEmpty(contract.name))
            return "Advertising campaign name is required";

        if (string.IsNullOrEmpty(contract.description))
            return "Advertising campaign description is required";

        if (contract.statusId == null)
            return "Advertising campaign status is required";

        if (contract.budget == null)
            return "Advertising campaign budget is required";

        if (contract.categoryId == null)
            return "Advertising campaign category is required";

        if (contract.clientId == null)
            return "Advertising campaign client is required";

        if (string.IsNullOrEmpty(contract.startDate.ToString()))
            return "Advertising campaign start date is required";

        if (string.IsNullOrEmpty(contract.endDate.ToString()))
            return "Advertising campaign end date is required";

        return null;
    }
}
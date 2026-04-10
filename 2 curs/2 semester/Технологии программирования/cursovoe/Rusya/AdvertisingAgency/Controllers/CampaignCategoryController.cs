using AdvertisingAgency.Contract.CampaignCategory;
using AdvertisingAgency.Model;
using AdvertisingAgency.Repository;
using Microsoft.AspNetCore.Mvc;
using Task = System.Threading.Tasks.Task;

namespace AdvertisingAgency.Controllers;

[ApiController]
[Route("api/campaign/categories")]
[Produces("application/json")]
public class CampaignCategoryController:ControllerBase
{
    private readonly CampaignCategoryRepository repository;

    public CampaignCategoryController(CampaignCategoryRepository repository)
    {
        this.repository = repository;
    }
    
    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<CampaignCategory>),
        StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<CampaignCategory>>> getAll()
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
    [ProducesResponseType(typeof(CampaignCategory),
        StatusCodes.Status404NotFound)]
    public async Task<ActionResult<CampaignCategory>> get(int id)
    {
        try
        {
            CampaignCategory campaignCategory = await repository.get(id);
            if (campaignCategory == null)
                return NotFound($"Campaign category with id {id} not found");

            return Ok(campaignCategory);
        }
        catch (InvalidOperationException)
        {
            return NotFound($"Campaign category with id {id} not found");
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Internal server error: {ex.Message}");
        }
    }

    [HttpPost]
    [ProducesResponseType(typeof(CampaignCategory), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<CampaignCategory>> create([FromBody] CampaignCategoryCreateContract categoryCreate)
    {
        try
        {
            if (categoryCreate == null)
                return BadRequest("Campaign category data is null");

            if (string.IsNullOrEmpty(categoryCreate.name))
                return BadRequest("Campaign category name is required");

            CampaignCategory campaignCategory = new CampaignCategory(0, categoryCreate.name);
            campaignCategory = await repository.save(campaignCategory);

            return CreatedAtAction(nameof(get),
                new { id = campaignCategory.id }, campaignCategory);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Internal server error: {ex.Message}");
        }
    }

    [HttpPut("{id}")]
    [ProducesResponseType(typeof(CampaignCategory), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<CampaignCategory>> update(int id,
        [FromBody] CampaignCategoryCreateContract categoryUpdate)
    {
        try
        {
            if (categoryUpdate == null)
                return BadRequest("Campaign category data is null");

            if (string.IsNullOrEmpty(categoryUpdate.name))
                return BadRequest("Campaign category name is required");

            CampaignCategory updatedCategory = await repository
                .update(id, new CampaignCategory(id, categoryUpdate.name));

            if (updatedCategory == null)
                return NotFound($"Campaign category with id {id} not found");

            return Ok(updatedCategory);
        }
        catch (InvalidOperationException)
        {
            return NotFound($"Campaign category with id {id} not found");
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
            else return NotFound($"Campaign category with id {id} not found");
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Internal server error: {ex.Message}");
        }
    }
}
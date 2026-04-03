using Lab5.DAO;
using Lab5.Models;
using Lab5.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace Lab5.Controllers;

public class AdvertisingCampaignsController:Controller
{
    private AdvertisingCampaignDAO campaignDao = new AdvertisingCampaignDAO();

    public ActionResult Index()
    {
        return View(campaignDao.getCampaigns());
    }

    public ActionResult Details(long id)
    {
        return View(campaignDao.getCampaign(id));
    }

    public ActionResult CreatePage()
    {
        CreateCampaignViewModel viewModel = new CreateCampaignViewModel
        (new AdvertisingCampaign(),
            new CampaignStatusesDAO().getStatuses(),
            new CampaignStatuses(),
            new ClientDAO().getClients(),
            new Client());
        
        return View(viewModel);
    }

    public ActionResult Create(CreateCampaignViewModel viewModel)
    {
        try
        {
            AdvertisingCampaign campaign =
                new AdvertisingCampaign(viewModel.campaign.name,
                    viewModel.campaign.description,
                    viewModel.campaign.startDate,
                    viewModel.campaign.endDate,
                    viewModel.campaign.budget,
                    viewModel.status,
                    viewModel.client);
            campaignDao.createCampaign(campaign);
            return RedirectToAction(nameof(Index));
        }
        catch
        {
            return View();
        }
    }

    public ActionResult EditPage(long id)
    {
        AdvertisingCampaign editingCampaign = campaignDao.getCampaign(id);
        CreateCampaignViewModel viewModel = new CreateCampaignViewModel
        (editingCampaign,
            new CampaignStatusesDAO().getStatuses(),
            editingCampaign.status,
            new ClientDAO().getClients(),
            editingCampaign.client);
        
        return View(viewModel);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public ActionResult Edit(long id, CreateCampaignViewModel viewModel)
    {
        try
        {
            AdvertisingCampaign campaign =
                new AdvertisingCampaign(viewModel.campaign.name,
                    viewModel.campaign.description,
                    viewModel.campaign.startDate,
                    viewModel.campaign.endDate,
                    viewModel.campaign.budget,
                    viewModel.status,
                    viewModel.client);
            campaignDao.updateCampaign(id,campaign);
            return RedirectToAction(nameof(Index));
        }
        catch
        {
            return View();
        }
    }

    public ActionResult Delete(long id)
    {
        try
        {
            campaignDao.deleteCampaign(id);
            return RedirectToAction(nameof(Index));
        }
        catch
        {
            return View();
        }
    }
}
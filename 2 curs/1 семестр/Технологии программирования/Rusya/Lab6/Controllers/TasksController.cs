using Lab6.DAO;
using Lab6.Models;
using Lab6.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Lab6.Controllers;

public class TasksController:Controller
{
    private TasksDAO _tasksDao = new TasksDAO();

    [Authorize(Roles = "Admin,Guest")]
    public ActionResult Index()
    {
        return View(_tasksDao.getTasks());
    }

    [Authorize(Roles = "Admin,Guest")]
    public ActionResult Details(long id)
    {
        return View(_tasksDao.getTask(id));
    }

    [Authorize(Roles = "Admin")]
    public ActionResult CreatePage()
    {
        CreateTaskViewModel viewModel = new CreateTaskViewModel
        (new Tasks(), new AdvertisingCampaignDAO().getCampaigns(),
            new TaskStatusesDAO().getStatuses(),
            new AdvertisingCampaign(),
            new TaskStatuses());

        return View(viewModel);
    }

    [Authorize(Roles = "Admin")]
    public ActionResult Create(CreateTaskViewModel viewModel)
    {
        try
        {
            Tasks task = new Tasks(viewModel.task.title,
                viewModel.task.description, viewModel.task.deadline,
                viewModel.status, viewModel.task.assignedTo, viewModel.campaign);
            _tasksDao.createTask(task);
            return RedirectToAction(nameof(Index));
        }
        catch
        {
            return View();
        }
    }

    [Authorize(Roles = "Admin")]
    public ActionResult EditPage(long id)
    {
        Tasks editingTask = _tasksDao.getTask(id);
        CreateTaskViewModel viewModel = new CreateTaskViewModel
        (editingTask,
            new AdvertisingCampaignDAO().getCampaigns(),
            new TaskStatusesDAO().getStatuses(),
            editingTask.campaign,
            editingTask.status);

        return View(viewModel);
    }

    [Authorize(Roles = "Admin")]
    [HttpPost]
    [ValidateAntiForgeryToken]
    public ActionResult Edit(long id, CreateTaskViewModel viewModel)
    {
        try
        {
            Tasks task = new Tasks(viewModel.task.title,
                viewModel.task.description,
                viewModel.task.deadline,
                viewModel.status,
                viewModel.task.assignedTo,
                viewModel.campaign);
            _tasksDao.updateTask(id, task);
            return RedirectToAction(nameof(Index));
        }
        catch
        {
            return View();
        }
    }

    [Authorize(Roles = "Admin")]
    public ActionResult Delete(long id)
    {
        try
        {
            _tasksDao.deleteTask(id);
            return RedirectToAction(nameof(Index));
        }
        catch
        {
            return View();
        }
    }
}
using Lab6.DAO;
using Lab6.Models;
using Lab6.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Lab6.Controllers;

public class TasksController : Controller
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
        TaskViewModel viewModel = new TaskViewModel
        (new Tasks(), new ProjectDAO().getAllProjects(),
            new TaskStatusesDAO().getStatuses(),
            new Projects(),
            new TaskStatuses());

        return View(viewModel);
    }

    [Authorize(Roles = "Admin")]
    public ActionResult Create(TaskViewModel viewModel)
    {
        try
        {
            Tasks task = new Tasks(viewModel.task.title,
                viewModel.task.description, viewModel.task.deadline,
                viewModel.status, viewModel.task.assignedTo, viewModel.project);
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
        TaskViewModel viewModel = new TaskViewModel
        (editingTask,
            new ProjectDAO().getAllProjects(),
            new TaskStatusesDAO().getStatuses(),
            editingTask.project,
            editingTask.status);

        return View(viewModel);
    }

    [Authorize(Roles = "Admin")]
    [HttpPost]
    [ValidateAntiForgeryToken]
    public ActionResult Edit(long id, TaskViewModel viewModel)
    {
        try
        {
            Tasks task = new Tasks(viewModel.task.title,
                viewModel.task.description,
                viewModel.task.deadline,
                viewModel.status,
                viewModel.task.assignedTo,
                viewModel.project);
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
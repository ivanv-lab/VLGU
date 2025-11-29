using Lab6.DAO;
using Lab6.Models;
using Lab6.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Lab6.Controllers;

public class ProjectsController : Controller
{
    private ProjectDAO projectDao = new ProjectDAO();

    [Authorize(Roles = "Admin,Guest")]
    public ActionResult Index()
    {
        return View(projectDao.getAllProjects());
    }

    [Authorize(Roles = "Admin,Guest")]
    public ActionResult Details(long id)
    {
        return View(projectDao.getProject(id));
    }

    [Authorize(Roles = "Admin")]
    public ActionResult CreatePage()
    {
        ProjectViewModel viewModel = new ProjectViewModel
        (new Projects(),
            new ProjectStatusDAO().getStatuses(),
            new ProjectStatuses(),
            new ClientDAO().getAllClients(),
            new Client());

        return View(viewModel);
    }

    [Authorize(Roles = "Admin")]
    public ActionResult Create(ProjectViewModel viewModel)
    {
        try
        {
            Projects project =
                new Projects(viewModel.project.name,
                    viewModel.project.description,
                    viewModel.project.startDate,
                    viewModel.project.endDate,
                    viewModel.project.budget,
                    viewModel.status,
                    viewModel.client);
            projectDao.createProject(project);
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
        Projects editingProject = projectDao.getProject(id);
        ProjectViewModel viewModel = new ProjectViewModel
        (editingProject,
            new ProjectStatusDAO().getStatuses(),
            editingProject.status,
            new ClientDAO().getAllClients(),
            editingProject.client);

        return View(viewModel);
    }

    [Authorize(Roles = "Admin")]
    [HttpPost]
    [ValidateAntiForgeryToken]
    public ActionResult Edit(long id, ProjectViewModel viewModel)
    {
        try
        {
            Projects project =
                new Projects(viewModel.project.name,
                    viewModel.project.description,
                    viewModel.project.startDate,
                    viewModel.project.endDate,
                    viewModel.project.budget,
                    viewModel.status,
                    viewModel.client);
            projectDao.updateProject(id, project);
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
            projectDao.deleteProject(id);
            return RedirectToAction(nameof(Index));
        }
        catch
        {
            return View();
        }
    }
}
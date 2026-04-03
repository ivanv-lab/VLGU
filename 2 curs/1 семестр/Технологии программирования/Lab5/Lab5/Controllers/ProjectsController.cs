using Lab5.DAO;
using Lab5.Models;
using Lab5.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace Lab5.Controllers;

public class ProjectsController:Controller
{
    private ProjectDAO projectDao = new ProjectDAO();

    public ActionResult Index()
    {
        return View(projectDao.getAllProjects());
    }

    public ActionResult Details(long id)
    {
        return View(projectDao.getProject(id));
    }

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
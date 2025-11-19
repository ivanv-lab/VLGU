using Lab6.DAO;
using Lab6.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Lab6.Controllers
{
    public class ClientsController : Controller
    {
        ClientDAO clientDAO=new ClientDAO();
        // GET: ClientController

        [Authorize(Roles = "Admin,Guest")]
        public ActionResult Index()
        {
            return View(clientDAO.getClients());
        }

        // GET: ClientController/Details/5
        [Authorize(Roles = "Admin,Guest")]
        public ActionResult Details(int id)
        {
            return View(clientDAO.getClient(id));
        }

        // GET: ClientController/Create
        [Authorize(Roles = "Admin")]
        public ActionResult CreatePage()
        {
            return View();
        }

        // POST: ClientController/Create
        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Client contract)
        {
            try
            {
                clientDAO.createClient(contract);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: ClientController/Edit/5
        [Authorize(Roles = "Admin")]
        public ActionResult EditPage(int id)
        {
            return View(clientDAO.getClient(id));
        }

        // POST: ClientController/Edit/5
        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, Client contract)
        {
            try
            {
                clientDAO.updateClient(id, contract);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        [Authorize(Roles = "Admin")]
        public ActionResult Delete(int id)
        {
            try
            {
                clientDAO.deleteClient(id);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}

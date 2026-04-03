using Lab5.DAO;
using Lab5.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Lab5.Controllers
{
    public class ClientsController : Controller
    {
        ClientDAO clientDAO=new ClientDAO();
        // GET: ClientController
        public ActionResult Index()
        {
            return View(clientDAO.getAllClients());
        }

        // GET: ClientController/Details/5
        public ActionResult Details(int id)
        {
            return View(clientDAO.getClient(id));
        }

        // GET: ClientController/Create
        public ActionResult CreatePage()
        {
            return View();
        }

        // POST: ClientController/Create
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
        public ActionResult EditPage(int id)
        {
            return View(clientDAO.getClient(id));
        }

        // POST: ClientController/Edit/5
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

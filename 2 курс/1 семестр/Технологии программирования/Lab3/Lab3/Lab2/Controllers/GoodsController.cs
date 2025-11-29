using Lab2.Connection;
using Lab2.Models;
using Microsoft.AspNetCore.Mvc;

namespace Lab2.Controllers
{
    public class GoodsController : Controller
    {
        private SqlConnection _connection=new SqlConnection();
        public IActionResult Index()
        {
            return View(_connection.readEnties());
        }

        public IActionResult CreatePage()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Good good)
        {
            _connection.createEntry(good);
            return RedirectToAction("Index");
        }

        public IActionResult Delete(int id)
        {
            _connection.removeEnry(id);
            return RedirectToAction("Index");
        }
    }
}

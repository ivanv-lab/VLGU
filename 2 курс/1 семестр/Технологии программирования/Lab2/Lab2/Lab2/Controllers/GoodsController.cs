using Lab2.Models;
using Microsoft.AspNetCore.Mvc;

namespace Lab2.Controllers
{
    public class GoodsController : Controller
    {
        static List<Good> goods = [];
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult CreatePage()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Good good)
        {
            goods.Add(good);
            return View("Index",goods);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id)
        {
            goods.RemoveAt(id);
            return View("Index", goods);
        }
    }
}

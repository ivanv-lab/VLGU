using Lab7.Models;
using Lab7.Repository;
using Microsoft.AspNetCore.Mvc;

namespace Lab7.Controllers
{
    public class GroupController:Controller
    {
        private readonly GroupRepository groupRepository;

        public GroupController(GroupRepository groupRepository)
        {
            this.groupRepository = groupRepository;
        }

        public ActionResult Index()
        {
            return View(groupRepository.getGroups());
        }

        public ActionResult Details(long id)
        {
            return View(groupRepository.getGroup(id));
        }

        public ActionResult CreatePage()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Group group)
        {
            return RedirectToAction("Index",groupRepository.add(group));
        }

        public ActionResult EditPage(long id)
        {
            return View(groupRepository.getGroup(id));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Group group)
        {
            return RedirectToActionPermanent("Index",groupRepository.update(group));
        }

        public ActionResult Delete(long id)
        {
            return RedirectToAction("Index",groupRepository.delete(id));
        }
    }
}

using Lab7.Models;
using Lab7.Models.ViewModels;
using Lab7.Repository;
using Microsoft.AspNetCore.Mvc;

namespace Lab7.Controllers
{
    public class ContactController:Controller
    {
        private readonly ContactRepository contactRepository;
        private readonly GroupRepository groupRepository;

        public ContactController(ContactRepository contactRepository,
            GroupRepository groupRepository)
        {
            this.contactRepository = contactRepository;
            this.groupRepository = groupRepository;
        }

        public ActionResult Index()
        {
            ContactViewModel contactViewModel = new ContactViewModel();
            contactViewModel.contacts = contactRepository.getContacts();
            contactViewModel.groups = groupRepository.getGroups();
            return View(contactViewModel);
        }

        public ActionResult FilterByGroup(long id)
        {
            ContactViewModel contactViewModel = new ContactViewModel();
            contactViewModel.contacts = contactRepository.getContactsByGroup(id);
            contactViewModel.groups = groupRepository.getGroups();
            return View("Index", contactViewModel);
        }

        public ActionResult Details(long id)
        {
            return View(contactRepository.getContact(id));
        }

        public ActionResult CreatePage()
        {
            ContactCreateModel model = new ContactCreateModel
                (new Contact(),groupRepository.getGroups());
            return View(model);
        }

        public ActionResult Create(Contact contact)
        {
            return RedirectToAction("Index",contactRepository.add(contact));
        }

        public ActionResult EditPage(long id)
        {
            Contact contact = contactRepository.getContact(id);
            ContactCreateModel model=new ContactCreateModel
                (contact,groupRepository.getGroups(),contact.group.id);
            return View(model);
        }

        public ActionResult Edit(Contact contact)
        {
            return RedirectToAction("Index",contactRepository.update(contact));
        }

        public ActionResult Delete(long id)
        {
            return RedirectToAction("Index",contactRepository.delete(id));
        }
    }
}

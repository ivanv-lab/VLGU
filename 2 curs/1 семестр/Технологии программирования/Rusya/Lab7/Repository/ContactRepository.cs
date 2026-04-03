using Lab7.Models;
using Microsoft.EntityFrameworkCore;

namespace Lab7.Repository
{
    public class ContactRepository
    {
        private readonly ApplicationDbContext _context;

        public ContactRepository(ApplicationDbContext context)
        {
            this._context = context;
        }

        public Contact getContact(long id)
        {
            return _context.Contacts
                .Where(c => c.id == id)
                .Include(c => c.group)
                .First();
        }

        public List<Contact> getContacts()
        {
            return _context.Contacts
                .Include(c => c.group)
                .ToList();
        }

        public List<Contact> getContactsByGroup(long groupId)
        {
            return _context.Contacts
                .Include(c => c.group)
                .Where(c => c.group.id == groupId)
                .ToList();
        }

        public Contact add(Contact contact)
        {
            if (contact.group != null && contact.group.id > 0)
            {
                Group group = _context.Groups
                    .Where(g => g.id == contact.group.id)
                    .First();
                contact.group = group;
            }
            else contact.group = null;

            _context.Contacts.Add(contact);
            _context.SaveChanges();
            return getContact(contact.id);
        }

        public Contact update(Contact contact)
        {
            Contact existingContact = getContact(contact.id);

            if (existingContact != null)
            {
                existingContact.firstName= contact.firstName;
                existingContact.secondName= contact.secondName;
                existingContact.email= contact.email;
                existingContact.phone= contact.phone;

                if(contact.group!=null && contact.group.id > 0)
                {
                    Group existingGroup = _context.Groups
                        .Find(contact.group.id);
                    existingContact.group= existingGroup;
                } else existingContact.group=null;
            }

            _context.SaveChanges();
            return getContact(contact.id);
        }

        public bool delete(long id)
        {
            try
            {
                Contact contact = getContact(id);
                _context.Remove(contact);
                _context.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}

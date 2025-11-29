using Lab7.Models;
using Microsoft.EntityFrameworkCore;

namespace Lab7.Repository
{
    public class GroupRepository
    {
        private readonly ApplicationDbContext _context;

        public GroupRepository(ApplicationDbContext context)
        {
            this._context=context;
        }

        public Group getGroup(long id)
        {
            return _context.Groups
                .Where(g=>g.id==id)
                .Include(g=>g.contacts)
                .First();
        }

        public List<Group> getGroups()
        {
            return _context.Groups
                .Include(g=>g.contacts)
                .ToList();
        }

        public Group add(Group group)
        {
            _context.Groups.Add(group);
            _context.SaveChanges();
            return getGroup(group.id);
        }

        public Group update(Group group)
        {
            _context.Entry(group).State=EntityState.Modified;
            _context.SaveChanges();
            return getGroup(group.id);
        }

        public bool delete(long id)
        {
            try
            {
                Group group = getGroup(id);
                _context.Remove(group);
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

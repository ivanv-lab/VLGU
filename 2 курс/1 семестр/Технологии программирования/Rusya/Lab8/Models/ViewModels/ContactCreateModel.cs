namespace Lab7.Models.ViewModels
{
    public class ContactCreateModel
    {
        public Contact contact {get;set;}
        public List<Group> groups {get;set;}

        public long groupId { get;set;}

        public ContactCreateModel() { }

        public ContactCreateModel(Contact contact, List<Group> groups)
        {
            this.contact = contact;
            this.groups = groups;
        }

        public ContactCreateModel(Contact contact, List<Group> groups, long groupId)
            : this(contact, groups)
        {
            this.groupId = groupId;
        }
    }
}

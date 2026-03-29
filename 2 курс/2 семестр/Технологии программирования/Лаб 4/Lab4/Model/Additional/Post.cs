using Lab4.Model.Main;

namespace Lab4.Model.Additional
{
    public class Post
    {
        public long id { get; set; }
        public string name { get; set; }

        public ICollection<DeaneryStaff> deaneryStaff {  get; set; }

        public Post(long id, string name)
        {
            this.id = id;
            this.name = name;
        }
    }
}

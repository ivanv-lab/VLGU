using Lab4.Model.Additional;

namespace Lab4.Model.Main
{
    public class Group
    {
        public long id { get; set; }
        public string groupCode { get; set; }
        public int yearOfEntry { get; set; }
        public long spetialtyId { get; set; }
        public long curriculumId { get; set; }

        public Spetialty spetialty{ get; set; }
        public Curriculum curriculum { get; set; }
    }
}

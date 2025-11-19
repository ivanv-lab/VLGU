namespace Lab5.Models
{
    public class Records
    {
        public int Id { get; set; }
        public string Title {  get; set; }
        public string Author {  get; set; }
        public DateTime PublicationDate { get; set; }
        public List<Records> RecordList { get; set; }

        public Records(int id, string title, string author, DateTime publicationDate)
        {
            Id = id;
            Title = title;
            Author = author;
            PublicationDate = publicationDate;
        }
    }
}

namespace Lab4.Model.Main
{
    public class Order
    {
        public long id { get; set; }
        public string orderNumber { get; set; }
        public DateOnly date { get; set; }
        public string contentFilePath { get; set; }
        public long applicationId {  get; set; }


        public Application application { get; set; }
    }
}

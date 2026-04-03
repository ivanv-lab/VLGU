namespace Lab4.Contract.Additional.ApplicationStatus
{
    public class ApplicationStatusCreateContract
    {
        public string name { get; set; }

        public ApplicationStatusCreateContract(string name)
        {
            this.name = name;
        }
    }
}

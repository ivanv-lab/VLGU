namespace AdvertisingAgency.Contract.TaskStatus;

public class TaskStatusCreateContract
{
    public string name { get; set; }

    public TaskStatusCreateContract(string name)
    {
        this.name = name;
    }
}
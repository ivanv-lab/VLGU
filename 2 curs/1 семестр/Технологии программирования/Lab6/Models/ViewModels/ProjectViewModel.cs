namespace Lab6.Models.ViewModels;

public class ProjectViewModel
{
    public Projects project { get; set; }
    public List<ProjectStatuses> statuses { get; set; }
    public ProjectStatuses status { get; set; }
    public List<Client> clients { get; set; }
    public Client client { get; set; }

    public ProjectViewModel()
    {
    }

    public ProjectViewModel(Projects project, List<ProjectStatuses> statuses, ProjectStatuses status, List<Client> clients, Client client)
    {
        this.project = project;
        this.statuses = statuses;
        this.status = status;
        this.clients = clients;
        this.client = client;
    }
}
namespace Lab6.Models.ViewModels;

public class TaskViewModel
{
    public Tasks task { get; set; }
    public List<Projects> projects { get; set; }
    public List<TaskStatuses> statuses { get; set; }
    public Projects project { get; set; }
    public TaskStatuses status { get; set; }

    public TaskViewModel()
    {
    }

    public TaskViewModel(Tasks task, List<Projects> projects, List<TaskStatuses> statuses, Projects project, TaskStatuses status)
    {
        this.task = task;
        this.projects = projects;
        this.statuses = statuses;
        this.project = project;
        this.status = status;
    }
}
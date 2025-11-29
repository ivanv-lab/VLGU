using System.ComponentModel.DataAnnotations;

namespace Lab5.Models
{
    public class Tasks
    {
        public long id {  get; set; }
        
        [Required(ErrorMessage ="Наименование задачи обязательно")]
        [StringLength(255,ErrorMessage ="Наименование задачи не должно превышать 255 символов")]
        [Display(Name ="Наименование задачи")]
        public string title {  get; set; }
        
        [Required(ErrorMessage ="Описание задачи обязательно")]
        [StringLength(255,ErrorMessage ="Описание задачи не должно превышать 255 символов")]
        [Display(Name ="Описание задачи")]
        public string description { get; set; }
        
        [Required(ErrorMessage ="Срок задачи обязательно")]
        [Display(Name ="Срок задачи")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}",ApplyFormatInEditMode = true)]
        public DateOnly deadline { get; set; }
        
        [Required(ErrorMessage ="Статус задачи обязательно")]
        [Display(Name ="Статус задачи")]
        public TaskStatuses status { get; set; }
        
        [Required(ErrorMessage ="Исполнитель задачи обязательно")]
        [StringLength(255,ErrorMessage ="Исполнитель задачи не должно превышать 255 символов")]
        [Display(Name ="Исполнитель задачи")]
        public string assignedTo { get; set; }
        
        [Required(ErrorMessage ="Проект задачи обязательно")]
        [StringLength(255,ErrorMessage ="Проект задачи не должно превышать 255 символов")]
        [Display(Name ="Проект задачи")]
        public Projects project { get; set; }

        public Tasks()
        {
        }

        public Tasks(string title, string description, DateOnly deadline, TaskStatuses status, string assignedTo, Projects project)
        {
            this.title = title;
            this.description = description;
            this.deadline = deadline;
            this.status = status;
            this.assignedTo = assignedTo;
            this.project = project;
        }

        public Tasks(long id, string title, string description, DateOnly deadline, TaskStatuses status, string assignedTo, Projects project)
        {
            this.id = id;
            this.title = title;
            this.description = description;
            this.deadline = deadline;
            this.status = status;
            this.assignedTo = assignedTo;
            this.project = project;
        }
        
        public Tasks(long id, string title, string description, DateOnly deadline, TaskStatuses status, string assignedTo)
        {
            this.id = id;
            this.title = title;
            this.description = description;
            this.deadline = deadline;
            this.status = status;
            this.assignedTo = assignedTo;
        }
    }
}

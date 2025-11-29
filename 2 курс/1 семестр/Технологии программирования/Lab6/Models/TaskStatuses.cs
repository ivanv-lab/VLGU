using System.ComponentModel.DataAnnotations;

namespace Lab6.Models
{
    public class TaskStatuses
    {
        public long id { get; set; }

        [Required(ErrorMessage = "Статус задачи обязательно")]
        [StringLength(255, ErrorMessage = "Статус задачи не должно превышать 255 символов")]
        [Display(Name = "Статус задачи")]
        public string name { get; set; }

        public TaskStatuses()
        {
        }

        public TaskStatuses(long id, string name)
        {
            this.id = id;
            this.name = name;
        }
    }
}

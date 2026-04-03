using System.ComponentModel.DataAnnotations;

namespace Lab5.Models
{
    public class ProjectStatuses
    {
        public long id {  get; set; }
        
        [Required(ErrorMessage ="Статус проекта обязательно")]
        [StringLength(255,ErrorMessage ="Статус проекта не должно превышать 255 символов")]
        [Display(Name ="Статус проекта")]
        public string name { get; set; }

        public ProjectStatuses()
        { }

        public ProjectStatuses(long id, String name)
        {
            this.id = id;
            this.name = name;
        }
    }
}

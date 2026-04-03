using System.ComponentModel.DataAnnotations;

namespace Lab7.Models
{
    public class Group
    {
        public long id {  get; set; }

        [Required(ErrorMessage = "Наименование группы")]
        [StringLength(255, ErrorMessage = "Наименование группы не должно превышать 255 символов")]
        [Display(Name = "Наименование группы")]
        public string name {  get; set; }

        [Required(ErrorMessage = "Описание группы обязательно")]
        [StringLength(1000, ErrorMessage = "Описание группы не должно превышать 1000 символов")]
        [Display(Name = "Описание группы")]
        public string description { get; set; }

        public virtual ICollection<Contact>? contacts { get; set; }
    }
}

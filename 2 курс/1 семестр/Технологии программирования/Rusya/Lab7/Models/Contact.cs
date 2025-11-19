using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Lab7.Models
{
    public class Contact
    {
        public long id {  get; set; }

        [Column("first_name")]
        [Required(ErrorMessage = "Имя контакта")]
        [StringLength(255, ErrorMessage = "Имя контакта не должно превышать 255 символов")]
        [Display(Name = "Имя контакта")]
        public string firstName {  get; set; }

        [Column("second_name")]
        [Required(ErrorMessage = "Фамилия контакта")]
        [StringLength(255, ErrorMessage = "Фамилия контакта не должно превышать 255 символов")]
        [Display(Name = "Фамилия контакта")]
        public string secondName { get; set; }

        [Required(ErrorMessage = "Номер телефона")]
        [StringLength(255, ErrorMessage = "Номер телефона не должно превышать 255 символов")]
        [Display(Name = "Номер телефона")]
        public string phone {  get; set; }

        [Required(ErrorMessage = "Email")]
        [StringLength(255, ErrorMessage = "Email не должно превышать 255 символов")]
        [Display(Name = "Email")]
        public string email { get; set; }

        public Group? group { get; set; }
    }
}

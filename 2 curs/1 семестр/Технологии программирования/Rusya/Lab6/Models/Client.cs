using System.ComponentModel.DataAnnotations;

namespace Lab6.Models
{
    public class Client
    {
        public long id { get; set; }
        [Required(ErrorMessage ="Наименование компании обязательно")]
        [StringLength(255,ErrorMessage ="Наименование компании не должно превышать 255 символов")]
        [Display(Name ="Наименование компании")]
        public string companyName { get; set; }
        
        [Required(ErrorMessage = "Контактное лицо обязательно")]
        [StringLength(255, ErrorMessage = "Контактное лицо не должно превышать 255 символов")]
        [Display(Name = "Контактное лицо")]
        public string contactPerson { get; set; }
        
        [Required(ErrorMessage = "Email обязательно")]
        [StringLength(255, ErrorMessage = "Email не должно превышать 255 символов")]
        [Display(Name = "Email")]
        public string email { get; set; }
        
        [Required(ErrorMessage = "Номер телефона обязательно")]
        [StringLength(11, ErrorMessage = "Номер телефона не должно превышать 255 символов")]
        [Display(Name = "Номер телефона")]
        public string phone {  get; set; }
        
        [Required(ErrorMessage = "Адрес обязательно")]
        [StringLength(255, ErrorMessage = "Адрес не должно превышать 255 символов")]
        [Display(Name = "Адрес")]
        public string address { get; set; }
        
        public List<AdvertisingCampaign> campaigns { get; set; }

        public Client() { }

        public Client(long id, string companyName, string contactPerson, string email, string phone, string address)
        {
            this.id = id;
            this.companyName = companyName;
            this.contactPerson = contactPerson;
            this.email = email;
            this.phone = phone;
            this.address = address;
        }

        public Client(string companyName,string contactPerson,string email,
            string phone,string address)
        {
            this.companyName = companyName;
            this.contactPerson = contactPerson;
            this.email = email;
            this.phone = phone;
            this.address = address;
        }
    }
}

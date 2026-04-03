using System.ComponentModel.DataAnnotations;

namespace Lab5.Models
{
    public class AdvertisingCampaign
    {
        public long id {  get; set; }
        
        [Required(ErrorMessage ="Наименование проекта обязательно")]
        [StringLength(255,ErrorMessage ="Наименование проекта не должно превышать 255 символов")]
        [Display(Name ="Наименование проекта")]
        public string name { get; set; }
        
        [Required(ErrorMessage ="Описание проекта обязательно")]
        [StringLength(255,ErrorMessage ="Описание проекта не должно превышать 255 символов")]
        [Display(Name ="Описание проекта")]
        public string description { get; set; }
        
        [Required(ErrorMessage ="Дата начала проекта обязательно")]
        [Display(Name ="Дата начала проекта")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}",ApplyFormatInEditMode = true)]
        public DateOnly startDate {  get; set; }
        
        [Required(ErrorMessage ="Дата окончания проекта обязательно")]
        [Display(Name ="Дата окончания проекта")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}",ApplyFormatInEditMode = true)]
        public DateOnly endDate { get; set; }
        
        [Required(ErrorMessage ="Бюджет проекта обязательно")]
        [Display(Name ="Бюджет проекта")]
        [Range(1000, Double.MaxValue,ErrorMessage = "Бюджет не может быть менее 1000")]
        public decimal budget {  get; set; }

        [Required(ErrorMessage ="Статус проекта обязательно")]
        [Display(Name ="Статус проекта")]
        public CampaignStatuses status { get; set; }
        
        [Required(ErrorMessage = "Клиент обязателен")]
        [Display(Name = "Клиент")]
        public Client client { get; set; }
        
        public List<Tasks> tasks { get; set; }

        public AdvertisingCampaign()
        {
        }

        public AdvertisingCampaign(string name, string description, DateOnly startDate, 
            DateOnly endDate, decimal budget, CampaignStatuses status, Client client)
        {
            this.name = name;
            this.description = description;
            this.startDate = startDate;
            this.endDate = endDate;
            this.budget = budget;
            this.status = status;
            this.client = client;
        }
        public AdvertisingCampaign(long id, string name, string description, DateOnly startDate, 
            DateOnly endDate, decimal budget, CampaignStatuses status, Client client)
        {
            this.id = id;
            this.name = name;
            this.description = description;
            this.startDate = startDate;
            this.endDate = endDate;
            this.budget = budget;
            this.status = status;
            this.client = client;
        }
        
        public AdvertisingCampaign(long id, string name, string description, DateOnly startDate, 
            DateOnly endDate, decimal budget, CampaignStatuses status)
        {
            this.id = id;
            this.name = name;
            this.description = description;
            this.startDate = startDate;
            this.endDate = endDate;
            this.budget = budget;
            this.status = status;
        }
    }
}

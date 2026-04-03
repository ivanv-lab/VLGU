using System.ComponentModel.DataAnnotations;

namespace Lab5.Models
{
    public class CampaignStatuses
    {
        public long id {  get; set; }
        
        [Required(ErrorMessage ="Статус проекта обязательно")]
        [StringLength(255,ErrorMessage ="Статус проекта не должно превышать 255 символов")]
        [Display(Name ="Статус проекта")]
        public string name { get; set; }

        public CampaignStatuses()
        { }

        public CampaignStatuses(string name)
        {
            this.name = name;
        }

        public CampaignStatuses(long id, String name)
        {
            this.id = id;
            this.name = name;
        }
    }
}

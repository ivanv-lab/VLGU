using System.ComponentModel.DataAnnotations;

namespace Lab2.Models
{
    public class Good
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Наименование товара обязательно")]
        [StringLength(255,ErrorMessage = "Наименование товара не должно превышать 255 символов")]
        [Display(Name = "Наименование товара")]
        public string Name { get; set; }

        [StringLength(1000, ErrorMessage = "Описание товара не должно превышать 1000 символов")]
        [Display(Name = "Описание товара")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Цена товара обязательна")]
        [Range(1,int.MaxValue,ErrorMessage ="Цена должна быть положительной")]
        [Display(Name="Цена")]
        public double Price { get; set; }

        [Required(ErrorMessage = "Кол-во товара обязательна")]
        [Range(0, int.MaxValue, ErrorMessage = "Кол-во должно быть положительным")]
        [Display(Name = "Кол-во")]
        public int Count {  get; set; }

        [Range(1,5,ErrorMessage ="Рейтинг должен быть от 1 до 5")]
        [Display(Name="Рейтинг")]
        public double Raiting {  get; set; }

        public Good() { 
            Random rand=new Random();
            this.Raiting=rand.Next(3,5);
            this.Id = counter;
            counter++;
        }

        private static int counter=0;
    }
}

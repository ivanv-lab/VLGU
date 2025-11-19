using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab2_8
{
    public abstract class Person
    {
        public string Name { get; set; }
        public string BirthDate {  get; set; }

        public Person(string name, string birthDate)
        {
            Name = name;
            BirthDate = birthDate;
        }

        public virtual string GetRole()
        {
            return "Не определено";
        }

        public override string ToString()
        {
            return "Имя: " + Name + " Дата рождения: " + BirthDate + " Роль: " + GetRole();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab9_2
{
    public class Man
    {
        public int Age { get; set; }
        public string Name {  get; set; }
        public Man(int age, string name)
        {
            Age = age;
            Name = name;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab2_8
{
    public class Pilot : Person
    {
        public string LicenseNumber { get; set; }
        public int FlightHourse {  get; set; }
        public Pilot(string name, string birthDate,
            string licenseNumber, int flightHours) : base(name, birthDate)
        {
            Console.WriteLine("\n" + DateTime.Now);
            Console.WriteLine("Создается пилот "+name);
            Thread.Sleep(2000);
            LicenseNumber = licenseNumber;
            FlightHourse = flightHours;
            Thread.Sleep(500);
            Console.WriteLine("Пилот "+name+" создан");
        }

        public override string GetRole()
        {
            Console.WriteLine("\n" + DateTime.Now);
            Console.WriteLine("Метод GetRole");
            Console.WriteLine("Метод возвращает роль человека");
            Console.WriteLine("Получаем данные...");
            Thread.Sleep(2000);
            return "Пилот";
        }

        public void FlyPlane(Airplane plane)
        {
            Console.WriteLine("\n" + DateTime.Now);
            Console.WriteLine("Получаем данные...");
            Thread.Sleep(2000);
            Console.WriteLine("Пилот " + Name + " управляет самолетом " + plane.Model);
            Console.WriteLine(DateTime.Now);
            Console.WriteLine("Метод FlyPlane");
            Console.WriteLine("Метод садит человека за штурвал самолета");
        }

        public override string ToString()=>
             "Пилот: "+base.ToString()+", Лицензия: "+LicenseNumber+", Часов налета: "+FlightHourse;
    }
}

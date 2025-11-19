using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab2_8
{
    public class Passenger : Person
    {
        public string PassportNumber { get; set; }
        public string SeatNumber { get; set; }

        public Passenger(string name, string birthDate, 
            string passpornNumber) : base(name, birthDate)
        { 
            Console.WriteLine("\n"+DateTime.Now);
            Console.WriteLine("Проверяется пассажир " + name);
            Thread.Sleep(10000);
            PassportNumber = passpornNumber;
            Thread.Sleep(500);
            Console.WriteLine("Пассажир " + name + " создан");
        }

        public override string GetRole()
        {
            Console.WriteLine("\n" + DateTime.Now);
            Console.WriteLine("Метод GetRole");
            Console.WriteLine("Метод возвращает роль человека");
            Console.WriteLine("Получаем данные...");
            Thread.Sleep(2000);
            return "Пассажир";
        }

        public void BoardPlane(Airplane plane, string seatNumber)
        {
            Console.WriteLine("\n" + DateTime.Now);
            Console.WriteLine("Пассажир "+Name+" садится на "+seatNumber+" место самолета "+plane.Model);
            Thread.Sleep(5000);
            SeatNumber = seatNumber;
            Console.WriteLine("Пассажир " + Name + " занял место " + seatNumber + " на самолете " + plane.Model);
            Console.WriteLine(DateTime.Now);
            Console.WriteLine("Метод BoardPlane");
            Console.WriteLine("Метод садит человека на борт самолета");
        }

        public override string ToString()=>
            "Пассажир: "+base.ToString()+", Паспорт: "+PassportNumber+", Место: "+SeatNumber;
    }
}

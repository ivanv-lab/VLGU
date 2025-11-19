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
            PassportNumber = passpornNumber;
        }

        public override string GetRole()
        {
            Console.WriteLine(DateTime.Now);
            Console.WriteLine("Метод GetRole");
            Console.WriteLine("Метод возвращает роль человека");
            return "Пассажир";
        }

        public void BoardPlane(Airplane plane, string seatNumber)
        {
            SeatNumber = seatNumber;
            Console.WriteLine("Пассажир " + Name + " занял место " + seatNumber + " на самолете " + plane.Model);
            Console.WriteLine(DateTime.Now);
            Console.WriteLine("Метод BoardPlane");
            Console.WriteLine("Метод садит человека на борт самолета");
        }

        public override string ToString()
        {
            return "Пассажир: "+base.ToString()+", Паспорт: "+PassportNumber+", Место: "+SeatNumber;
        }
    }
}

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
            return "Пассажир";
        }

        public void BoardPlane(Airplane plane, string seatNumber)
        {
            SeatNumber = seatNumber;
            Console.WriteLine("Пассажир "+Name+" занял место "+seatNumber+" на самолете "+plane.Model);
        }

        public override string ToString()
        {
            return "Пассажир: "+base.ToString()+", Паспорт: "+PassportNumber+", Место: "+SeatNumber;
        }
    }
}

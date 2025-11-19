using Lab2_8;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab7_2
{
    public class Mailer
    {
        public static void SendFlightNotify(Flight flight)
        {
            Console.WriteLine("Информация о рейсе::::::::::::::");
            Console.WriteLine("Номер рейса: "+flight.FlightNumber);
            Console.WriteLine("Модель самолета: "+flight.Airplane.Model);
            Console.WriteLine("Место назначения: " + flight.Destination);
            Console.WriteLine("Список пассажиров:::");
            flight.Passengers.ForEach(p =>
            {
                Console.WriteLine(p.Name);
            });
            Console.WriteLine("::::::");
            Console.WriteLine("Номер лицензии пилота: "+flight.Pilot.LicenseNumber);
            Console.WriteLine("ФИО пилота: "+flight.Pilot.Name);
            Console.WriteLine("::::::::::::::::::::::::::::::::");
        }
    }
}

using Lab7_2;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Lab2_8
{
    public delegate void FlightProcessor(Flight flight);
    public class Flight
    {
        public string FlightNumber { get; set; }
        public string Origin { get; set; }
        public string Destination { get; set; }
        public DateTime DepartureTime { get; set; }
        public Airplane Airplane { get; set; }
        public Pilot Pilot { get; set; }
        public List<Passenger> Passengers { get; set; }

        public Flight(string flightNumber, string origin, string destination, DateTime departureTime, Airplane airplane, Pilot pilot)
        {
            Console.WriteLine("\n" + DateTime.Now);
            Console.WriteLine("Создается рейс "+flightNumber+"...");
            Thread.Sleep(5000);
            FlightNumber = flightNumber;
            Origin = origin;
            Destination = destination;
            DepartureTime = departureTime;
            Airplane = airplane;
            Pilot = pilot;
            Passengers = new List<Passenger>();
            Console.WriteLine("Рейс "+flightNumber+" создан");
        }

        public void AddPassenger(Passenger passenger)
        {
            if (!Airplane.Passengers.Contains(passenger))
            {
                Console.WriteLine($"Нельзя добавить пассажира {passenger.Name}. Сначала добавьте пассажира в самолет!");
                return;
            }
            Console.WriteLine("Пассажир " + passenger.Name + " добавляется в рейс");
            Console.WriteLine("\n" + DateTime.Now);
            Thread.Sleep(5000);
            Passengers.Add(passenger);
            Console.WriteLine($"Пассажир {passenger.Name} добавлен на рейс {FlightNumber}.");
            Console.WriteLine(DateTime.Now);
            Console.WriteLine("Метод AddPassenger");
            Console.WriteLine("Метод добавляет пассажиров на рейс");
        }

        public override string ToString()=>
             $"Рейс: {FlightNumber}, {Origin} - {Destination}, Время вылета: {DepartureTime}, Самолет: {Airplane.Model}, Пилот: {Pilot.Name}";

        public static void Go(Flight flight)=>
            Console.WriteLine("Рейс отправлен");
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab2_8
{
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
            FlightNumber = flightNumber;
            Origin = origin;
            Destination = destination;
            DepartureTime = departureTime;
            Airplane = airplane;
            Pilot = pilot;
            Passengers = new List<Passenger>();
        }

        public void AddPassenger(Passenger passenger)
        {
            if (!Airplane.Passengers.Contains(passenger))
            {
                Console.WriteLine($"Нельзя добавить пассажира {passenger.Name}. Сначала добавьте пассажира в самолет!");
                return;
            }

            Passengers.Add(passenger);
            Console.WriteLine($"Пассажир {passenger.Name} добавлен на рейс {FlightNumber}.");
        }

        public override string ToString()
        {
            return $"Рейс: {FlightNumber}, {Origin} - {Destination}, Время вылета: {DepartureTime}, Самолет: {Airplane.Model}, Пилот: {Pilot.Name}";
        }
    }
}

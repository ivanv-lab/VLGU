using Lab2_8;

namespace Lab7_2
{
    internal class Program
    {
        static void Main(string[] args)
        {

            Pilot pilot1 = new Pilot("Иван Иванов", "1980-05-10", "ABC-123", 5000);
            Passenger passenger1 = new Passenger("Анна Петрова", "1995-12-20", "XYZ-456");
            Passenger passenger2 = new Passenger("Петр Сидоров", "1988-08-15", "UVW-789");
            Airplane boeing737 = new Airplane("Boeing 737", 180, 4000);
            Flight flight1 = new Flight("SU100", "Москва", "Санкт-Петербург", DateTime.Now.AddHours(2), boeing737, pilot1);

            FlightEvent go = new FlightEvent();
            go.GoHandle +=
                new FlightEvent.FlightHandler(Flight.Go);
            go.GoHandle +=
                new FlightEvent.FlightHandler(Mailer.SendFlightNotify);

            boeing737.AddPassenger(passenger1);
            boeing737.AddPassenger(passenger2);

            pilot1.FlyPlane(boeing737);
            passenger1.BoardPlane(boeing737, "12A");
            flight1.AddPassenger(passenger1);
            flight1.AddPassenger(passenger2);

            go.Go(flight1);

        }
    }
}

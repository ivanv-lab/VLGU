using Lab2_8;
using Lab7_2;

namespace Lab10_2
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            Task<Pilot> pilotTask = Task.Run(() =>
            new Pilot("Иван Иванов", "1980-05-10", "ABC-123", 5000));
            Task<Passenger> passenger1Task = Task.Run(() =>
            new Passenger("Анна Петрова", "1995-12-20", "XYZ-456"));
            Task<Passenger> passenger2Task = Task.Run(() =>
            new Passenger("Петр Сидоров", "1988-08-15", "UVW-789"));
            Task<Airplane> airplaneTask = Task.Run(() =>
            new Airplane("Boeing 737", 180, 4000));

            await Task.WhenAll(pilotTask, passenger1Task, passenger2Task, airplaneTask);

            Pilot pilot1 = pilotTask.Result;
            Passenger passenger1 = passenger1Task.Result;
            Passenger passenger2 = passenger2Task.Result;
            Airplane boeing747 = airplaneTask.Result;

            Task<Flight> flight1 = Task.Run(() =>
            new Flight("SU100", "Москва", "Санкт-Петербург", DateTime.Now.AddHours(2), boeing747, pilot1));

            FlightEvent go = new FlightEvent();
            go.GoHandle += new FlightEvent.FlightHandler(Flight.Go);
            go.GoHandle+= new FlightEvent.FlightHandler(Mailer.SendFlightNotify);

            await Task.WhenAll(flight1);

            Flight flight = flight1.Result;

            Task add1 = Task.Run(() => boeing747.AddPassenger(passenger1));
            Task add2=Task.Run(() => boeing747.AddPassenger(passenger2));

            Task fly=Task.Run(() => pilot1.FlyPlane(boeing747));

            Task<Passenger> passenger3Task = Task.Run(() =>
            new Passenger("Никита Колач", "1987-09-03", "RRTN-56-2"));

            Task board = Task.Run(() =>
            passenger3Task.Result.BoardPlane(boeing747,"6"));

            await Task.WhenAll(add1, add2, fly, board);

            Task addPass1 = Task.Run(() => flight.AddPassenger(passenger1));
            Task addPass2=Task.Run(()=>flight.AddPassenger(passenger2));

            await Task.WhenAll(addPass1, addPass2);

            go.Go(flight);
        }
    }
}

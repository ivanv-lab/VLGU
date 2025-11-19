using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Lab2_8
{
    public class Airplane
    {
        public string Model { get; set; }
        public int Capacity { get; set; }
        public int RangeKm { get; set; }
        public List<Passenger> Passengers { get; set; }

        public Airplane(string model, int capacity, int rangeKm)
        {
            Console.WriteLine("\n" + DateTime.Now);
            Console.WriteLine("Добавляется самолет "+model);
            Model = model;
            Capacity = capacity;
            RangeKm = rangeKm;
            Passengers = new List<Passenger>();
            Thread.Sleep(5000);
            Console.WriteLine("Самолет "+model+" добавлен.");
        }

        public void AddPassenger(Passenger passenger)
        {
            if (Passengers.Count < Capacity)
            {
                Console.WriteLine("Пассажир " + passenger.Name + " садится на борт самолета " + Model);
                Console.WriteLine("\n" + DateTime.Now);
                Thread.Sleep(5000);
                Passengers.Add(passenger);
                Console.WriteLine("Пассажир " + passenger.Name + " добавлен на борт самолета " + Model);
                Console.WriteLine(DateTime.Now);
                Console.WriteLine("Метод AddPassenger");
                Console.WriteLine("Метод добавляет пассажира на борт самолета");
            }
            else
            {
                Console.WriteLine("Самолет заполнен!");
            }
        }

        public void RemovePassenger(Passenger passenger)
        {
            if (Passengers.Contains(passenger))
            {
                Console.WriteLine("Пассажир " + passenger.Name + " удаляется с борта" + Model);
                Console.WriteLine("\n" + DateTime.Now);
                Thread.Sleep(5000);
                Passengers.Remove(passenger);
                Console.WriteLine("Пассажир " + passenger.Name + " удален с борта самолета " + Model);
                Console.WriteLine(DateTime.Now);
                Console.WriteLine("Метод RemovePassenger");
                Console.WriteLine("Метод удаляет пассажира с борта самолета");
            }
            else
            {
                Console.WriteLine("Пассажира " + passenger.Name + " нет на борту самолета " + Model);
            }
        }
    }
}

using Lab5.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab5.Classes
{
    public class Airplane:IAirplane
    {
        public string Model { get; set; }
        public int Capacity { get; set; }
        public int RangeKm { get; set; }
        public List<Passenger> Passengers { get; set; }

        public Airplane(string model, int capacity, int rangeKm)
        {
            Model = model;
            Capacity = capacity;
            RangeKm = rangeKm;
            Passengers = new List<Passenger>();
        }

        public void AddPassenger(Passenger passenger)
        {
            if (Passengers.Count < Capacity)
            {
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

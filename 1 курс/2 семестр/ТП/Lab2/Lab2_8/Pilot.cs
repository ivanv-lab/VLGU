using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab2_8
{
    public class Pilot : Person
    {
        public string LicenseNumber { get; set; }
        public int FlightHourse {  get; set; }
        public Pilot(string name, string birthDate,
            string licenseNumber, int flightHours) : base(name, birthDate)
        {
            LicenseNumber = licenseNumber;
            FlightHourse = flightHours;
        }

        public override string GetRole()
        {
            return "Пилот";
        }

        public void FlyPlane(Airplane plane)
        {
            Console.WriteLine("Пилот "+Name+" управляет самолетом "+plane.Model);
        }

        public override string ToString()
        {
            return "Пилот: "+base.ToString()+", Лицензия: "+LicenseNumber+", Часов налета: "+FlightHourse;
        }
    }
}

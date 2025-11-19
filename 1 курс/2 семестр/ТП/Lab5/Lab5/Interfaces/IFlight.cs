using Lab5.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab5.Interfaces
{
    public interface IFlight
    {
        public void AddPassenger(Passenger passenger);
        public string ToString();
    }
}

using Lab2_8;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab7_2
{
    public class FlightEvent
    {
        public delegate void FlightHandler(Flight flight);
        public event FlightHandler? GoHandle;
        public void Go(Flight flight)
        {
            if (GoHandle != null) GoHandle(flight);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Parkering
{
    public class Bus : Vehicle
    {
        public int Passengers { get; set; }

        public Bus(string type, string licensePlate, string color, int passengers) : base(licensePlate, color)
        {
            Type = type;
            Passengers = passengers;

        }
        
    }
}

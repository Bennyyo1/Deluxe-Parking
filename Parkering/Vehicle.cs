using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Parkering
{
    public abstract class Vehicle
    {
        public string Type { get; set; }
        public string LicensePlate { get; set; }
        public string Color { get; set; }

        public Vehicle(string licensePlate, string color) //constructor
        {
            LicensePlate = licensePlate;
            Color = color;
        }
        
    }
}

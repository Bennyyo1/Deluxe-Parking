using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Parkering
{
    public class Motorcycle : Vehicle
    {
        public string Brand { get; set; }

        public Motorcycle(string type, string licensePlate, string color, string brand) : base(licensePlate, color)
        {
            Type = type;
            Brand = brand;
        }
        
    }
}

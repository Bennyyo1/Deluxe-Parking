using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Parkering
{
    public class Car : Vehicle
    {
        public bool IsElectric { get; set; }

        public Car(string type, string licensePlate, string color, bool isElectric) : base(licensePlate, color)
        {
            Type=type;
            IsElectric = isElectric;
        }
        
       
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Homework2.model
{
    public class CargoVehicle : Vehicle
    {
        public int MaxWeight { get; set; }
        public CargoVehicle(int max_weight)
        {
            MaxWeight = max_weight;
        }
    }
}

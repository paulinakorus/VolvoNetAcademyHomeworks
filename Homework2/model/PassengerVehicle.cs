using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Homework2.model
{
    public class PassengerVehicle : Vehicle
    {
        public int MaxWeight { get; set; }
        public PassengerVehicle(int max_weight)
        {
            MaxWeight = max_weight;
        }
    }
}

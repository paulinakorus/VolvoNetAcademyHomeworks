using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Homework2.model
{
    public class CargoVehicle : Vehicle
    {
        //public int MaxWeight { get; set; }
        public static List<CargoVehicle> cargoVehicleList { get; set; } = new List<CargoVehicle>();
        public CargoVehicle(/*int max_weight*/)
        {
            //MaxWeight = max_weight;
            cargoVehicleList.Add(this);
        }
    }
}

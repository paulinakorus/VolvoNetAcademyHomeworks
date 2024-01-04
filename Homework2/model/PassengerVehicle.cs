using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Homework2.model
{
    public class PassengerVehicle : Vehicle
    {
        public float AverageLesseeRating { get; set; }
        public static List<PassengerVehicle> passengerVehicleList { get; set; } = new List<PassengerVehicle>();

        public PassengerVehicle() 
        {
            AverageLesseeRating = 0.0f;
            passengerVehicleList.Add(this);
        }

        public static List<PassengerVehicle> GetPassengerVehicleList()
        {
            return passengerVehicleList;
        }
    }
}

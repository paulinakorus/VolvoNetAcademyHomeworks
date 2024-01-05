using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Homework2.model
{
    public class PassengerVehicle : Vehicle
    {
        public double AverageLesseeRating { get; set; }
        public static List<PassengerVehicle> passengerVehicleList { get; set; } = new List<PassengerVehicle>();
        private double sumOfLesseeRatings;
        private int numberOfLesseeRatings;
        public PassengerVehicle() 
        {
            AverageLesseeRating = 0.0d;
            passengerVehicleList.Add(this);
            sumOfLesseeRatings = 0.0d;
            numberOfLesseeRatings = 0;
        }

        public void synchronizeAverageLesseeRating(double rating)
        {
            AverageLesseeRating = (sumOfLesseeRatings+rating)/(++numberOfLesseeRatings);
        }
    }
}

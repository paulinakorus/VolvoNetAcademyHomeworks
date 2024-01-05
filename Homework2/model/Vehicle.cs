using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Homework2.model
{
    public class Vehicle
    {
        private static int lastId = 0;
        public int Id { get; set; }
        public string Brand { get; set; }
        public string Model { get; set; }
        public int YearOfManufacture { get; set; }
        public string Color { get; set; }
        public int Price { get; set; }
        public string RegistrationNumber { get; set; }
        public int TravelDistance { get; set; }
        public int DurationOfTheTrips { get; set; }
        public double ModelSpecificCoefficient { get; set; }
        public static List<Vehicle> vehicleList { get; set; } = new List<Vehicle>();

        public Vehicle()
        {
            Id = lastId++;
            vehicleList.Add(this);
            TravelDistance = 0;
            DurationOfTheTrips = 0;
        }

        public void synchronizeToRent(Rent rent)
        {
            TravelDistance += rent.TravelDistance;
            DurationOfTheTrips += rent.DurationOfTheTrip;
        }
    }
}

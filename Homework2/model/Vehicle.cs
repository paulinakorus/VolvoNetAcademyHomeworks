using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Homework2.model
{
    public class Vehicle
    {
        public static int Id { get; set; }
        public string Brand { get; set; }
        public string Model { get; set; }
        public int YearOfManufacture { get; set; }
        public string Color { get; set; }
        public double Price { get; set; }
        public string RegistrationNumber { get; set; }
        public int TravelDistance { get; set; }
        public int DurationOfTheTrips { get; set; }
        public float ModelSpecificCoefficient { get; set; }

    }
}

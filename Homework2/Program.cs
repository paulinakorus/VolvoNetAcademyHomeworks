using Homework2.model;
using Homework2.service;
using System.Text;

namespace Homework2
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string beginningPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            string vehiclePath = Path.Combine(beginningPath, "VehicleFile.txt");
            string rentalPath = Path.Combine(beginningPath, "RentalFile.txt");
            GeneratingData generator = new GeneratingData(vehiclePath, rentalPath);

            generator.GeneratingVehicles();
            List<Vehicle> vehicles = Vehicle.vehicleList;
        }
    }
}
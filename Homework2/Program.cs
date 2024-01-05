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
            string passengerVehiclePath = Path.Combine(beginningPath, "PassengerVehicleFile.txt");
            string cargoVehiclePath = Path.Combine(beginningPath, "CargoVehicleFile.txt");
            string passengerRentalPath = Path.Combine(beginningPath, "PassengerRentalFile.txt");
            string cargoRentalPath = Path.Combine(beginningPath, "CargoRentalFile.txt");
            
            Container container = new Container();
            GeneratingData generator = new GeneratingData(passengerVehiclePath, cargoVehiclePath, passengerRentalPath, cargoRentalPath, container);

            //generator.GeneratingVehicles();
            //generator.GeneratingRentals();

            //container.RentVehicle();

            //generator.GeneratingRentals();
            List<Vehicle> vehicles = container.VehicleListOfBrand();

        }
    }
}
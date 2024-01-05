using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Homework2.model
{
    public class Container
    {
        public List<PassengerVehicle> PassengerList { get; set; }
        public List<CargoVehicle> CargoList { get; set; }
        public List<RentPassengerVehicle> PassengerRentalList { get; set; }
        public List<RentCargoVehicle> CargoRentalList { get; set; }

        private static string beginningPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
        private static string passengerVehiclePath = Path.Combine(beginningPath, "PassengerVehicleFile.txt");
        private static string cargoVehiclePath = Path.Combine(beginningPath, "CargoVehicleFile.txt");
        private static string passengerRentalPath = Path.Combine(beginningPath, "PassengerRentalFile.txt");
        private static string cargoRentalPath = Path.Combine(beginningPath, "CargoRentalFile.txt");

        public Container() 
        { 
            PassengerList = new List<PassengerVehicle>();
            CargoList = new List<CargoVehicle>();
            PassengerRentalList = new List<RentPassengerVehicle>();
            CargoRentalList = new List<RentCargoVehicle>();
        }

        public void AddVehicle(Vehicle vehicle)
        {

        }

        public void RemoveVehicle(Vehicle vehicle)
        {

        }

        public void RentVehicle()
        {

        }

        public void ReadVehicleFile()
        {
            string passengerFile = File.ReadAllText(passengerVehiclePath);
            string cargoFile = File.ReadAllText(cargoVehiclePath);
            PassengerList = JsonSerializer.Deserialize<List<PassengerVehicle>>(passengerFile);
            CargoList = JsonSerializer.Deserialize<List<CargoVehicle>>(cargoFile);
        }

        public void ReadRentalFile()
        {

        }
    }
}

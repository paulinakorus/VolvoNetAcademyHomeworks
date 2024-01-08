using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Transactions;
using Homework2.model;

namespace Homework2.service
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

        private int CorrectInt(string beforeText)
        {
            string text;
            var outText = 0;
            var correctText = false;
            var quantity = 0;
            while (!correctText)
            {
                if (++quantity != 1)
                    Console.WriteLine("Wrong number. Try again, please");
                Console.Write(beforeText);

                text = Console.ReadLine();
                correctText = int.TryParse(text, out outText);
            }
            return outText;
        }

        private double CorrectDouble(string beforeText)
        {
            string text;
            var outText = 0.0d;
            var correctText = false;
            var quantity = 0;
            while (!correctText)
            {
                if (++quantity != 1)
                    Console.WriteLine("Wrong number. Try again, please");
                Console.Write(beforeText);

                text = Console.ReadLine();
                correctText = double.TryParse(text, out outText);
            }
            return outText;
        }

        public void TypeVehicleAndAddToFile()
        {
            Console.WriteLine("\nTyping vehicle");
            Console.WriteLine("\t1 - passenger vehicle ");
            Console.WriteLine("\t2 - cargo vehicle");
            
            string type = null;
            int quantity = 0;
            while (type != "1" && type != "2"){
                if (++quantity != 1)
                    Console.WriteLine("Wrong symbol. Try again, please");

                Console.WriteLine("\nPlease insert the number of the type");
                Console.Write("\ttype: ");
                type = Console.ReadLine();
            }
            
            if(type == "1")
            {
                Console.WriteLine("\nPassenger vehicle");
                PassengerVehicle passengerVehicle = new PassengerVehicle();
                passengerVehicle = (PassengerVehicle)TypeVehicleData(passengerVehicle);
                AddVehicleToList(passengerVehicle);
            }else                                                                                   //(type == "2")
            {
                Console.WriteLine("\tCargo vehicle");
                CargoVehicle cargoVehicle = new CargoVehicle();
                cargoVehicle = (CargoVehicle)TypeVehicleData(cargoVehicle);
                AddVehicleToList(cargoVehicle);
            }
            WriteVehiclesToFiles();
        }

        private Vehicle TypeVehicleData(Vehicle obj)
        {
            Console.Write("\tbrand: ");
            obj.Brand = Console.ReadLine();
            Console.Write("\tmodel: ");
            obj.Model = Console.ReadLine();
            obj.YearOfManufacture = CorrectInt("\tyear of manufacture: ");
            Console.Write("\tcolor: ");
            obj.Color = Console.ReadLine();
            obj.Price = CorrectInt("\tprice: ");
            Console.Write("\tregistration number: ");
            obj.RegistrationNumber = Console.ReadLine();
            obj.ModelSpecificCoefficient = CorrectDouble("\tmodel specific coefficient: ");

            return obj;
        }

        private void AddVehicleToList(Vehicle vehicle)
        {
            if(vehicle.GetType() == typeof(PassengerVehicle))
            {
                PassengerList.Add((PassengerVehicle)vehicle);
            }
            else if(vehicle.GetType() == typeof(CargoVehicle))
            {
                CargoList.Add((CargoVehicle)vehicle);
            }
        }

        public void RemoveVehicle()
        {
            Console.WriteLine("\nRemoving vehicle");
            int ID = CorrectInt("\tid of vehicle: ");

            ReadVehicleFiles();
            foreach(PassengerVehicle vehicle in PassengerList)
            {
                if(vehicle.Id == ID)
                {
                    PassengerList.Remove(vehicle);
                    WriteVehiclesToFiles();
                    return;
                }
            }

            foreach (CargoVehicle vehicle in CargoList)
            {
                if (vehicle.Id == ID)
                {
                    CargoList.Remove(vehicle);
                    WriteVehiclesToFiles();
                    return;
                }
            }
            Console.WriteLine($"Vehicle with id: {ID} do not exist");
        }

        public void RentVehicle()
        {
            Console.WriteLine("\nRenting vehicle");
            int ID = CorrectInt("\tid of vehicle: ");

            ReadVehicleFiles();
            foreach (PassengerVehicle vehicle in PassengerList)
            {
                if (vehicle.Id == ID)
                {
                    RentPassengerVehicle rentPassenger = new RentPassengerVehicle();
                    Console.WriteLine("Passenger vehicle");
                    TypeRent(rentPassenger, ID);

                    rentPassenger.LesseeRating = CorrectDouble("\tlessee rating: ");
                    vehicle.synchronizeAverageLesseeRating(rentPassenger.LesseeRating);
                    vehicle.synchronizeToRent(rentPassenger);

                    PassengerRentalList.Add(rentPassenger);
                    WriteRentalsToFiles();
                    WriteVehiclesToFiles();

                    return;
                }
            }

            foreach (CargoVehicle vehicle in CargoList)
            {
                if (vehicle.Id == ID)
                {
                    RentCargoVehicle rentCargo = new RentCargoVehicle();
                    Console.WriteLine("Cargo vehicle");
                    TypeRent(rentCargo, ID);

                    rentCargo.Weight = CorrectInt("\tweight: ");

                    vehicle.synchronizeToRent(rentCargo);

                    CargoRentalList.Add(rentCargo);
                    WriteRentalsToFiles();
                    WriteVehiclesToFiles();

                    return;
                }
            }
            Console.WriteLine($"Vehicle with id: {ID} do not exist");
        }

        private void TypeRent(Rent rent, int ID)
        {
            rent.VehicleId = ID;
            rent.DurationOfTheTrip = CorrectInt("\tduration of the trip: ");
            rent.TravelDistance = CorrectInt("\ttravel distance: ");
        }

        public void WriteVehiclesToFiles()
        {
            if (PassengerList.Count != 0 || CargoList.Count != 0)
            {
                using (StreamWriter passengerInput = new StreamWriter(passengerVehiclePath, false))
                {
                    passengerInput.Write(JsonSerializer.Serialize(PassengerList));
                    passengerInput.Close();
                }

                using (StreamWriter cargoInput = new StreamWriter(cargoVehiclePath, false))
                {
                    cargoInput.Write(JsonSerializer.Serialize(CargoList));
                    cargoInput.Close();
                }
            }
            else
            {
                Console.WriteLine("Lists are empty, please insert information");
            }
        }

        public void ReadVehicleFiles()
        {
            var passengerFile = new FileInfo(passengerVehiclePath);
            var cargoFile = new FileInfo(cargoVehiclePath);
            bool ifPassengerFileExist = passengerFile.Exists;
            bool ifCargoFileExist = cargoFile.Exists;

            if (ifPassengerFileExist && ifCargoFileExist)
            {
                string passengerTextFile = File.ReadAllText(passengerVehiclePath);
                string cargoTextFile = File.ReadAllText(cargoVehiclePath);
                PassengerList = JsonSerializer.Deserialize<List<PassengerVehicle>>(passengerTextFile);
                CargoList = JsonSerializer.Deserialize<List<CargoVehicle>>(cargoTextFile);
            }
            else
            {
                Console.WriteLine("Files don't exist, please insert information");
            }
        }

        public void WriteRentalsToFiles()
        {
            if (PassengerRentalList.Count != 0 || CargoRentalList.Count != 0)
            {
                using (StreamWriter passengerInput = new StreamWriter(passengerRentalPath, false))
                {
                    passengerInput.Write(JsonSerializer.Serialize(PassengerRentalList));
                    passengerInput.Close();
                }

                using (StreamWriter cargoInput = new StreamWriter(cargoRentalPath, false))
                {
                    cargoInput.Write(JsonSerializer.Serialize(CargoRentalList));
                    cargoInput.Close();
                }
            }
            else
            {
                Console.WriteLine("Lists are empty, please insert information");
            }
        }
        public void ReadRentalFiles()
        {
            var passengerFile = new FileInfo(passengerRentalPath);
            var cargoFile = new FileInfo(cargoRentalPath);
            bool ifPassengerFileExist = passengerFile.Exists;
            bool ifCargoFileExist = cargoFile.Exists;

            if (ifPassengerFileExist && ifCargoFileExist)
            {
                string passengerTextFile = File.ReadAllText(passengerRentalPath);
                string cargoTextFile = File.ReadAllText(cargoRentalPath);
                PassengerRentalList = JsonSerializer.Deserialize<List<RentPassengerVehicle>>(passengerTextFile);
                CargoRentalList = JsonSerializer.Deserialize<List<RentCargoVehicle>>(cargoTextFile);
            }
            else
            {
                Console.WriteLine("Files don't exist, please insert information");
            }
        }
    }
}

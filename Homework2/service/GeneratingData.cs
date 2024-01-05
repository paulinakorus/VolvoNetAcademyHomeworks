using Homework2.model;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Homework2.service
{
    public class GeneratingData
    {
        public int PassengerVehicleNumber { get; set; }
        public int CargoVehicleNumber { get; set; }
        public int PassengerVehicleRentalNumber { get; set; }
        public int CargoVehicleRentalNumber { get; set; }
        public string PassengerVehicleFilePath { get; set; }
        public string CargoVehicleFilePath { get; set; }
        public string PassengerRentalFilePath { get; set; }
        public string CargoRentalFilePath { get; set; }
        private Random random;

        public GeneratingData(string passengerVehicleFilePath, string cargoVehicleFilePath, string passengerRentalFilePath, string cargoRentalFilePath)
        {
            PassengerVehicleFilePath = passengerVehicleFilePath;
            CargoVehicleFilePath = cargoVehicleFilePath;
            PassengerRentalFilePath = passengerRentalFilePath;
            CargoRentalFilePath = cargoRentalFilePath;
            random = new Random();
        }

        public void GeneratingVehicles()
        {
            Console.WriteLine("Generating Vehicles");
            Console.Write("\tpassenger vehicles: ");
            PassengerVehicleNumber = Convert.ToInt32(Console.ReadLine());

            Console.Write("\tcargo vehicles: ");
            CargoVehicleNumber = Convert.ToInt32(Console.ReadLine());

            var passengerFile = new FileInfo(PassengerVehicleFilePath);
            var cargoFile = new FileInfo(CargoVehicleFilePath);
            bool ifPassengerFileExist = passengerFile.Exists;
            bool ifCargoFileExist = cargoFile.Exists;
            using (StreamWriter passengerInput = new StreamWriter(PassengerVehicleFilePath, ifPassengerFileExist))
            {
                List<PassengerVehicle> passengerVehicleList = new List<PassengerVehicle>();
                for (int i = 0; i < PassengerVehicleNumber; i++)
                {
                    PassengerVehicle passengerVehicle = new PassengerVehicle();
                    passengerVehicle = (PassengerVehicle)GenerateVehicleData(passengerVehicle);
                    passengerVehicleList.Add(passengerVehicle);
                }
                passengerInput.Write(JsonSerializer.Serialize(passengerVehicleList));
                passengerInput.Close();
            }

            using (StreamWriter cargoInput = new StreamWriter(CargoVehicleFilePath, ifCargoFileExist))
            {
                List<CargoVehicle> cargoVehicleList = new List<CargoVehicle>();
                for (int i = 0; i < CargoVehicleNumber; i++)
                {
                    CargoVehicle cargoVehicle = new CargoVehicle(/*random.Next(1, 10) * 1000*/);
                    cargoVehicle = (CargoVehicle)GenerateVehicleData(cargoVehicle);
                    cargoVehicleList.Add(cargoVehicle);
                }
                cargoInput.Write(JsonSerializer.Serialize(cargoVehicleList));
                cargoInput.Close();
            }
        }

        public string GeneratingRegistrationNumber()
        {
            string registration = "";
            char[] letters = "ABCDEFGHIJKLMNOPQRSTUVWXYZ".ToCharArray();
            char[] allChars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789".ToCharArray();

            for (int i = 0; i < 3; i++)
                registration += letters[random.Next(0, letters.Length - 1)];
            registration += " ";
            for (int i = 0; i < 5; i++)
                registration += allChars[random.Next(0, allChars.Length - 1)];

            return registration;
        }

        public Vehicle GenerateVehicleData(Vehicle obj)
        {
            string[] brandsExamples = { "Toyota", "Volvo", "Ferrari", "Mazda" };
            string[] modelExamples = { "Campry", "Corolla", "Highlamnder", "Tacoma" };
            string[] colorExamples = { "black", "grey", "blue", "white", "red" };

            obj.Brand = brandsExamples[random.Next(0, brandsExamples.Length - 1)];
            obj.Model = modelExamples[random.Next(0, modelExamples.Length - 1)];
            obj.YearOfManufacture = 2000 + random.Next(0, 24);
            obj.Color = colorExamples[random.Next(0, colorExamples.Length - 1)];
            obj.Price = random.Next(1, 20) * 10000;
            obj.RegistrationNumber = GeneratingRegistrationNumber();
            obj.ModelSpecificCoefficient = 1.00 + random.NextDouble();

            return obj;
        }

        public void GeneratingRentals()
        {
            Console.WriteLine("\nGenerating Rentals");
            Console.Write("\tpassenger vehicles rentals: ");
            PassengerVehicleRentalNumber = Convert.ToInt32(Console.ReadLine());

            Console.Write("\tcargo vehicles rentals: ");
            CargoVehicleRentalNumber = Convert.ToInt32(Console.ReadLine());

            var passengerFile = new FileInfo(PassengerRentalFilePath);
            var cargoFile = new FileInfo(CargoRentalFilePath);
            bool ifPassengerFileExist = passengerFile.Exists;
            bool ifCargoFileExist = cargoFile.Exists;
            using (StreamWriter passengerInput = new StreamWriter(PassengerRentalFilePath, ifPassengerFileExist))
            {
                List<PassengerVehicle> passengerVehiclesList = PassengerVehicle.passengerVehicleList;
                List<RentPassengerVehicle> rentPassengerVehicleList = new List<RentPassengerVehicle>();

                for (int i = 0; i < PassengerVehicleRentalNumber; i++)
                {
                    RentPassengerVehicle rentPassengerVehicle = new RentPassengerVehicle();

                    int randomIndex = random.Next(0, passengerVehiclesList.Count - 1);
                    rentPassengerVehicle.VehicleId = passengerVehiclesList[randomIndex].Id;
                    rentPassengerVehicle.DurationOfTheTrip = random.Next(1, 24);
                    rentPassengerVehicle.TravelDistance = random.Next(1, 20) * 100;
                    rentPassengerVehicle.LesseeRating = random.Next(1, 50) / 10.0d;

                    rentPassengerVehicleList.Add(rentPassengerVehicle);
                }
                passengerInput.Write(JsonSerializer.Serialize(rentPassengerVehicleList));
                passengerInput.Close();
            }

            using (StreamWriter cargoInput = new StreamWriter(CargoRentalFilePath, ifCargoFileExist))
            {
                List<CargoVehicle> cargoVehiclesList = CargoVehicle.cargoVehicleList;
                List<RentCargoVehicle> rentCargoVehicleList = new List<RentCargoVehicle>();

                for (int i = 0; i < CargoVehicleRentalNumber; i++)
                {
                    RentCargoVehicle rentCargoVehicle = new RentCargoVehicle();
                    rentCargoVehicle.Weight = random.Next(1, 10) * 1000;

                    int randomIndex = random.Next(0, cargoVehiclesList.Count - 1);
                    /*while (cargoVehiclesList[randomIndex].MaxWeight < rentCargoVehicle.Weight)
                    {
                        randomIndex = random.Next(0, cargoVehiclesList.Count - 1);
                    }*/

                    rentCargoVehicle.VehicleId = cargoVehiclesList[randomIndex].Id;
                    rentCargoVehicle.DurationOfTheTrip = random.Next(1, 24);
                    rentCargoVehicle.TravelDistance = random.Next(1, 20) * 100;

                    rentCargoVehicleList.Add(rentCargoVehicle);
                }
                cargoInput.Write(JsonSerializer.Serialize(rentCargoVehicleList));
                cargoInput.Close();
            }
        }
    }
}

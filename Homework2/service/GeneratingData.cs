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
        public Container VehicleContainer { get; set; }
        private Random random;

        public GeneratingData(string passengerVehicleFilePath, string cargoVehicleFilePath, string passengerRentalFilePath, string cargoRentalFilePath, Container container)
        {
            PassengerVehicleFilePath = passengerVehicleFilePath;
            CargoVehicleFilePath = cargoVehicleFilePath;
            PassengerRentalFilePath = passengerRentalFilePath;
            CargoRentalFilePath = cargoRentalFilePath;
            VehicleContainer = container;
            random = new Random();
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

        public void GeneratingVehicles()
        {
            var passengerFile = new FileInfo(PassengerVehicleFilePath);
            var cargoFile = new FileInfo(CargoVehicleFilePath);
            bool ifPassengerFileExist = passengerFile.Exists;
            bool ifCargoFileExist = cargoFile.Exists;

            if (ifPassengerFileExist && ifCargoFileExist)
            {
                VehicleContainer.ReadVehicleFiles();
            }
                
            Console.WriteLine("\nGenerating Vehicles");
            PassengerVehicleNumber = CorrectInt("\tpassenger vehicles: ");
            CargoVehicleNumber = CorrectInt("\tcargo vehicles: ");

            for (int i = 0; i < PassengerVehicleNumber; i++)
            {
                PassengerVehicle passengerVehicle = new PassengerVehicle();
                passengerVehicle = (PassengerVehicle)GenerateVehicleData(passengerVehicle);
                VehicleContainer.PassengerList.Add(passengerVehicle);
            }
            
            for (int i = 0; i < CargoVehicleNumber; i++)
            {
                CargoVehicle cargoVehicle = new CargoVehicle();
                cargoVehicle = (CargoVehicle)GenerateVehicleData(cargoVehicle);
                VehicleContainer.CargoList.Add(cargoVehicle);
            }
            VehicleContainer.WriteVehiclesToFiles();
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
            obj.PutComfortClass();
            obj.RegistrationNumber = GeneratingRegistrationNumber();
            obj.ModelSpecificCoefficient = 1.00 + random.NextDouble();

            return obj;
        }

        public void GeneratingRentals()                                                         // synchronize to rent
        {
            Console.WriteLine("\nGenerating Rentals");
            PassengerVehicleRentalNumber = CorrectInt("\tpassenger vehicles rentals: ");
            CargoVehicleRentalNumber = CorrectInt("\tcargo vehicles rentals: ");

            var passengerFile = new FileInfo(PassengerRentalFilePath);
            var cargoFile = new FileInfo(CargoRentalFilePath);
            bool ifPassengerFileExist = passengerFile.Exists;
            bool ifCargoFileExist = cargoFile.Exists;

            if (ifPassengerFileExist && ifCargoFileExist)
            {
                VehicleContainer.ReadRentalFiles();
            }

            for (int i = 0; i < PassengerVehicleRentalNumber; i++)
            {
                RentPassengerVehicle rentPassengerVehicle = new RentPassengerVehicle();

                int randomIndex = random.Next(0, VehicleContainer.PassengerList.Count - 1);
                PassengerVehicle vehicle = VehicleContainer.PassengerList[randomIndex];

                rentPassengerVehicle.VehicleId = VehicleContainer.PassengerList[randomIndex].Id;
                rentPassengerVehicle.DurationOfTheTrip = random.Next(1, 24);
                rentPassengerVehicle.TravelDistance = random.Next(1, 20) * 100;
                rentPassengerVehicle.LesseeRating = random.Next(1, 50) / 10.0d;

                vehicle.synchronizeToRent(rentPassengerVehicle);
                vehicle.synchronizeAverageLesseeRating(rentPassengerVehicle.LesseeRating);
                VehicleContainer.WriteVehiclesToFiles();
                VehicleContainer.PassengerRentalList.Add(rentPassengerVehicle);
            }
            

            for (int i = 0; i < CargoVehicleRentalNumber; i++)
            {
                RentCargoVehicle rentCargoVehicle = new RentCargoVehicle();
                rentCargoVehicle.Weight = random.Next(1, 10) * 1000;

                int randomIndex = random.Next(0, VehicleContainer.CargoList.Count - 1);
                CargoVehicle vehicle = VehicleContainer.CargoList[randomIndex];

                rentCargoVehicle.VehicleId = VehicleContainer.CargoList[randomIndex].Id;
                rentCargoVehicle.DurationOfTheTrip = random.Next(1, 24);
                rentCargoVehicle.TravelDistance = random.Next(1, 20) * 100;

                vehicle.synchronizeToRent(rentCargoVehicle);
                VehicleContainer.WriteVehiclesToFiles();
                VehicleContainer.CargoRentalList.Add(rentCargoVehicle);
            }
            VehicleContainer.WriteRentalsToFiles();
        }
    }
}

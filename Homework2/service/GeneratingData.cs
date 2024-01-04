using Homework2.model;
using System;
using System.Collections.Generic;
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
        public string VehicleFilePath { get; set; }
        public string RentalFilePath { get; set; }
        private Random random;

        public GeneratingData(string vehicleFilePath, string rentalFilePath)
        {
            VehicleFilePath = vehicleFilePath;
            RentalFilePath = rentalFilePath;
            random = new Random();
        }

        public void GeneratingVehicles()
        {
            Console.WriteLine("Generating Vehicles\n");
            Console.Write("\tpassenger vehicles: ");
            PassengerVehicleNumber = Convert.ToInt32(Console.ReadLine());

            Console.Write("\tcargo vehicles: ");
            CargoVehicleNumber = Convert.ToInt32(Console.ReadLine());

            var file = new FileInfo(VehicleFilePath);
            bool ifVehicleFileExist = file.Exists;
            using (StreamWriter vehicleFile = new StreamWriter(VehicleFilePath, ifVehicleFileExist))
            {
                for (int i = 0; i < PassengerVehicleNumber; i++)
                {
                    Vehicle PassengerVehicle = new PassengerVehicle();
                    PassengerVehicle = GenerateVehicleData(PassengerVehicle);
                    vehicleFile.WriteLine(JsonSerializer.Serialize(PassengerVehicle));
                }

                for (int i = 0; i < CargoVehicleNumber; i++)
                {
                    Vehicle CargoVehicle = new PassengerVehicle();
                    CargoVehicle = GenerateVehicleData(CargoVehicle);
                    vehicleFile.WriteLine(JsonSerializer.Serialize(CargoVehicle));
                }
                vehicleFile.Close();
            }
        }

        public void GeneratingRentals()
        {
            Console.WriteLine("Generating Rentals\n");
            Console.Write("\tpassenger vehicles rentals: ");
            PassengerVehicleRentalNumber = Convert.ToInt32(Console.ReadLine());

            Console.Write("\tcargo vehicles rentals: ");
            CargoVehicleRentalNumber = Convert.ToInt32(Console.ReadLine());
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
    }
}

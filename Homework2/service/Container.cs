﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
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

        public void TypeVehicleAndAddToFile()
        {
            Console.WriteLine("Typing vehicle");
            Console.WriteLine("\t1 - passenger vehicle ");
            Console.WriteLine("\t2 - cargo vehicle");
            
            string type = null;
            do
            {
                Console.WriteLine("Please insert the number of the type");
                Console.Write("\ttype: ");
                type = Console.ReadLine();
            } while (type != "1" && type != "2");
            
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

        public Vehicle TypeVehicleData(Vehicle obj)
        {
            Console.Write("\tbrand: ");
            obj.Brand = Console.ReadLine();
            Console.Write("\tmodel: ");
            obj.Model = Console.ReadLine();
            Console.Write("\tyear of manufacture: ");
            obj.YearOfManufacture = Convert.ToInt32(Console.ReadLine());
            Console.Write("\tcolor: ");
            obj.Color = Console.ReadLine();
            Console.Write("\tprice: ");
            obj.Price = Convert.ToInt32(Console.ReadLine());
            Console.Write("\tregistration number: ");
            obj.RegistrationNumber = Console.ReadLine();
            Console.Write("\tmodel specific coefficient: ");
            obj.ModelSpecificCoefficient = Convert.ToDouble(Console.ReadLine());

            return obj;
        }

        public void AddVehicleToList(Vehicle vehicle)
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

        public void RemoveVehicle(Vehicle vehicle)
        {
            ReadVehicleFiles();
            if (vehicle.GetType() == typeof(PassengerVehicle))
            {
                PassengerList.Remove((PassengerVehicle)vehicle);
            }
            else if (vehicle.GetType() == typeof(CargoVehicle))
            {
                CargoList.Remove((CargoVehicle)vehicle);
            }
            WriteVehiclesToFiles();
        }

        public void RemoveRent(Rent rent)
        {

        }

        public void RentVehicle()
        {

        }

        public void WriteVehiclesToFiles()
        {
            if (PassengerList.Count != 0 && CargoList.Count != 0)
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
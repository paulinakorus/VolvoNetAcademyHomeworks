using Homework2.model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Homework2.service
{
    public class DataSearcher
    {
        Container Container { get; set; }
        public DataSearcher(Container container)
        {
            Container = container;
        }

        public List<Vehicle> VehicleListOfBrand()
        {
            Container.ReadVehicleFiles();

            Console.WriteLine("List of vehicles from a inserted brand");
            Console.Write("\tbrand: ");
            string insertedBrand = Console.ReadLine();

            var brandPassengerList = Container.PassengerList
                .OrderBy(x => x.Brand)
                .Where(x => x.Brand == insertedBrand)
                .ToList();

            var brandCargoList = Container.CargoList
                .OrderBy(x => x.Brand)
                .Where(x => x.Brand == insertedBrand)
                .ToList();

            List<Vehicle> resultList = new List<Vehicle>();
            foreach (var vehicle in brandPassengerList)
            {
                resultList.Add(vehicle);
            }
            foreach (var vehicle in brandCargoList)
            {
                resultList.Add(vehicle);
            }
            return resultList;
        }

        public List<Vehicle> PredeterminedVehiclesList()
        {
            Container.ReadVehicleFiles();

            Console.WriteLine("List of predetermined vehicles of the model");
            Console.Write("\tmodel: ");
            string insertedModel = Console.ReadLine();

            var predeterminedPassengerList = Container.PassengerList
                .OrderBy(x => x.Model)
                .Where(x => x.Model == insertedModel)
                .Where(x => x.TravelDistance > 100000 || (DateTime.Now.Year - x.YearOfManufacture) > 5)
                .ToList();

            var predeterminedCargoList = Container.CargoList
               .OrderBy(x => x.Model)
               .Where(x => x.Model == insertedModel)
               .Where(x => x.TravelDistance > 100000000 || (DateTime.Now.Year - x.YearOfManufacture) > 15)
               .ToList();

            List<Vehicle> resultList = new List<Vehicle>();
            foreach (var vehicle in predeterminedPassengerList)
            {
                resultList.Add(vehicle);
            }
            foreach (var vehicle in predeterminedCargoList)
            {
                resultList.Add(vehicle);
            }
            return resultList;
        }

        public List<Vehicle> ServiceNeededVehiclesList()
        {
            Container.ReadVehicleFiles();

            Console.WriteLine("List of vehicles which need service soon");

            var neededPassengerList = Container.PassengerList
                .Where(x => (((x.TravelDistance) % 5000) >= (5000 - 1000)))
                .ToList();

            var neededCargoList = Container.CargoList
               .Where(x => (((x.TravelDistance) % 15000) >= (15000 - 1000)))
               .ToList();

            List<Vehicle> resultList = new List<Vehicle>();
            foreach (var vehicle in neededPassengerList)
            {
                resultList.Add(vehicle);
            }
            foreach (var vehicle in neededCargoList)
            {
                resultList.Add(vehicle);
            }
            return resultList;
        }

        public List<Vehicle> VehicleListofBrandAndColor()
        {
            Container.ReadVehicleFiles();

            Console.WriteLine("List of vehicles from a inserted brand with a inserted color");
            Console.Write("\tbrand: ");
            string insertedBrand = Console.ReadLine();
            Console.Write("\tcolor: ");
            string insertedColor = Console.ReadLine();


            var brandAndColorPassengerList = Container.PassengerList
                .OrderBy(x => x.ComfortClass)
                .Where(x => x.Brand == insertedBrand)
                .Where(x => x.Color == insertedColor)
                .ToList();

            var brandAndColorCargoList = Container.CargoList
               .OrderBy(x => x.ComfortClass)
               .Where(x => x.Brand == insertedBrand)
               .Where(x => x.Color == insertedColor)
               .ToList();

            List<Vehicle> resultList = new List<Vehicle>();

            foreach (var vehicle in brandAndColorPassengerList)
            {
                resultList.Add(vehicle);
            }
            foreach (var vehicle in brandAndColorCargoList)
            {
                resultList.Add(vehicle);
            }

            return resultList;
        }

        public double TotalValueOfVehicles()
        {
            Container.ReadVehicleFiles();

            var totalValue = 0.0d;
            double yearLose;
            double oneValue;

            foreach (var vehicle in Container.PassengerList)
            {
                yearLose = (vehicle.GetType() == typeof(PassengerVehicle)) ? 0.1 : 0.07;
                oneValue = VehicleValue(vehicle, yearLose);
                totalValue += oneValue;
            }
            foreach (var vehicle in Container.CargoList)
            {
                yearLose = (vehicle.GetType() == typeof(PassengerVehicle)) ? 0.1 : 0.07;
                oneValue = VehicleValue(vehicle, yearLose);
                totalValue += oneValue;
            }

            return totalValue;
        }
        private double VehicleValue(Vehicle vehicle, double losePerYear)
        {
            double totalValue = vehicle.Price;

            for (int i = vehicle.YearOfManufacture; i <= DateTime.Now.Year; i++)
            {
                if (totalValue >= (losePerYear * vehicle.Price))
                    totalValue -= (losePerYear * vehicle.Price);
                else
                    return 0;
            }
            return totalValue;
        }
    }
}

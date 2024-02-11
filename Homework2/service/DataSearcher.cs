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
            List<Vehicle> vehicleList = Container.GetWholeVehicleList();

            Console.WriteLine("\nList of vehicles from a inserted brand");
            Console.Write("\tbrand: ");
            string insertedBrand = Console.ReadLine();

            var resultList = vehicleList
                .OrderBy(x => x.Brand)
                .Where(x => x.Brand == insertedBrand)
                .ToList();

            return resultList;
        }

        public List<Vehicle> PredeterminedVehiclesList()
        {
            Container.ReadVehicleFiles();

            Console.WriteLine("\nList of predetermined vehicles of the model");
            Console.Write("\tmodel: ");
            string insertedModel = Console.ReadLine();

            var predeterminedPassengerList = Container.PassengerList
                .OrderBy(x => x.Model)
                .Where(x => x.Model == insertedModel)
                .Where(x => x.TravelDistance > 100000 || (DateTime.Now.Year - x.YearOfManufacture) > 5)
                .ToList();

            var greaterThan5 = new int[] { 1, 2, 6, 8, 2, 7, 9, 2, 4, 6, 2, 6, 8, 9 }.OrderBy(x => x).TakeWhile(x => x <= 5);

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
            List<Vehicle> vehicleList = Container.GetWholeVehicleList();

            Console.WriteLine("\nList of vehicles which need service soon");

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
            List<Vehicle> vehicleList = Container.GetWholeVehicleList();

            Console.WriteLine("\nList of vehicles from a inserted brand with a inserted color");
            Console.Write("\tbrand: ");
            string insertedBrand = Console.ReadLine();
            Console.Write("\tcolor: ");
            string insertedColor = Console.ReadLine();


            var resultList = vehicleList
                .Where(x => x.Brand == insertedBrand)
                .Where(x => x.Color == insertedColor)
                .OrderBy(x => x.ComfortClass)
                .ToList();

            return resultList;
        }

        public decimal TotalValueOfVehicles()
        {
            Container.ReadVehicleFiles();

            var totalValue = Container.PassengerList.Select(v => v.GetVehicleMonetaryValue()).Sum();
            //totalValue += 
            //foreach (var vehicle in Container.CargoList)
            //{
            //    yearLose = (vehicle.GetType() == typeof(CargoVehicle)) ? 0.1 : 0.07;
            //    oneValue = VehicleValue(vehicle, yearLose);
            //    totalValue += oneValue;
            //}

            return totalValue;
        }

    }
}

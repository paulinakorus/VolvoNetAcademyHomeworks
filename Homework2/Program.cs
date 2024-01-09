using Homework2.model;
using Homework2.service;
using System.IO;
using System.Text;
using System.Text.Json;
using System.Xml;
using Newtonsoft.Json;
using System.Numerics;

/*
    change lists to one
    view lists
*/
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
            FileInfo[] files = { new FileInfo(passengerVehiclePath), new FileInfo(cargoVehiclePath), new FileInfo(passengerRentalPath), new FileInfo(cargoRentalPath) };

            DelateFiles(files);

            Container container = new Container();
            GeneratingData generator = new GeneratingData(passengerVehiclePath, cargoVehiclePath, passengerRentalPath, cargoRentalPath, container);
            DataSearcher searcher = new DataSearcher(container);

            bool continuing = true;
            while (continuing)
            {
                Console.Clear();
                Console.WriteLine("Homework 2");
                Console.WriteLine("Leasing Company");

                Console.WriteLine("\ta\t-\tadd vehicle");
                Console.WriteLine("\tb\t-\tremove vehicle");
                Console.WriteLine("\tc\t-\trent vehicle");
                Console.WriteLine("\td\t-\tgenerate vehicles");
                Console.WriteLine("\te\t-\tgenerate rents");
                Console.WriteLine("\tf\t-\tlist inventory of vehicles of specified brand");
                Console.WriteLine("\tg\t-\tlist of predetermined vehicles of a chosen model");
                Console.WriteLine("\th\t-\tcalculate total vehicle fleet value");
                Console.WriteLine("\ti\t-\tlist of vehicles of a chosen brand and color");
                Console.WriteLine("\tj\t-\tlist of vehicles that are within 1000 km of requiring maintenance");

                bool wrongAction = true;
                while (wrongAction)
                {
                    Console.WriteLine("\nPlease enter the symbol of the function");
                    Console.Write("\tfunction: ");
                    string function = Console.ReadLine();
                    function = function.ToUpper();

                    switch (function)
                    {
                        case "A":
                            container.TypeVehicleAndAddToFile();
                            wrongAction = false;
                            break;
                        case "B":
                            container.RemoveVehicle();
                            wrongAction = false;
                            break;
                        case "C":
                            container.RentVehicle();
                            wrongAction = false;
                            break;
                        case "D":
                            generator.GeneratingVehicles();
                            wrongAction = false;
                            break;
                        case "E":
                            generator.GeneratingRentals();
                            wrongAction = false;
                            break;
                        case "F":
                            if (VehiclesFileExists(files))
                            {
                                ViewList(searcher.VehicleListOfBrand());
                                wrongAction = false;
                            }
                            else
                            {
                                Console.WriteLine("Files does not exist. Try again, please");
                                wrongAction = true;
                            }
                            break;
                        case "G":
                            if (VehiclesFileExists(files))
                            {
                                ViewList(searcher.PredeterminedVehiclesList());
                                wrongAction = false;
                            }
                            else
                            {
                                Console.WriteLine("Files does not exist. Try again, please");
                                wrongAction = true;
                            }
                            break;
                        case "H":
                            if (VehiclesFileExists(files))
                            {
                                Console.WriteLine($"\ttotal vehicle fleet value: {searcher.TotalValueOfVehicles()}");
                                wrongAction = false;
                            }
                            else
                            {
                                Console.WriteLine("Files does not exist. Try again, please");
                                wrongAction = true;
                            }
                            break;
                        case "I":
                            if (VehiclesFileExists(files))
                            {
                                ViewList(searcher.VehicleListofBrandAndColor());
                                wrongAction = false;
                            }
                            else
                            {
                                Console.WriteLine("Files does not exist. Try again, please");
                                wrongAction = true;
                            }
                            break;
                        case "J":
                            if (VehiclesFileExists(files))
                            {
                                ViewList(searcher.ServiceNeededVehiclesList());
                                wrongAction = false;
                            }
                            else
                            {
                                Console.WriteLine("Files does not exist. Try again, please");
                                wrongAction = true;
                            }
                            break;
                        default:
                            Console.WriteLine("Wrong symbol. Try again, please");
                            wrongAction = true;
                            break;
                    }
                }
                continuing = Answer();
                Console.ReadLine();
            }
            Console.WriteLine("The end of the program");
        }

        static bool Answer()
        {
            bool correct = false;
            while (!correct)
            {
                Console.WriteLine("\nDo you want to continue?");
                Console.Write("\tanswer (yes/no): ");

                var ans = Console.ReadLine();
                ans = ans.ToUpper();

                switch (ans)
                {
                    case "YES":
                        Console.WriteLine("Please enter any key");
                        return true;
                    case "NO":
                        Console.WriteLine("Please enter any key");
                        return false;
                    default:
                        Console.WriteLine("Incorrect answer");
                        Console.WriteLine("Try again\n");
                        correct = false;
                        break;
                }
            }
            Console.WriteLine("Please enter any key");
            return false;
        }

        static void DelateFiles(FileInfo[] files)
        {
            foreach (FileInfo file in files)
            {
                if (file.Exists)
                    File.Delete(file.FullName);
            }
        }

        static bool VehiclesFileExists(FileInfo[] files)
        {
            FileInfo[] fileInfos = files;
            bool exist = false;
            for (int i = 0; i < 2; i++)
            {
                bool oneExist = fileInfos[i].Exists;
                if (oneExist)
                {
                    exist = true;
                    break;
                }
            }
            return exist;
        }

        static void ViewList(List<Vehicle> list)
        {
            if(list.Count != 0)
            {
                foreach (Object obj in list)
                {
                    Console.WriteLine(JsonConvert.SerializeObject(obj, (Newtonsoft.Json.Formatting)System.Xml.Formatting.Indented));
                }
            }
            else
                Console.WriteLine("No specified vehicles in the List");
        }
    }
}
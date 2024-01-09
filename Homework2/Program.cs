using Homework2.model;
using Homework2.service;
using System.Text;

/*
    change lists to one
    view lists
    comfort class

*/
namespace Homework2
{
    internal class Program
    {
        private static string beginningPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
        private static string passengerVehiclePath = Path.Combine(beginningPath, "PassengerVehicleFile.txt");
        private static string cargoVehiclePath = Path.Combine(beginningPath, "CargoVehicleFile.txt");
        private static string passengerRentalPath = Path.Combine(beginningPath, "PassengerRentalFile.txt");
        private static string cargoRentalPath = Path.Combine(beginningPath, "CargoRentalFile.txt");
        private static FileInfo[] files = { new FileInfo(passengerVehiclePath), new FileInfo(cargoVehiclePath), new FileInfo(passengerRentalPath), new FileInfo(cargoRentalPath) };
        static void Main(string[] args)
        {

            DelateFiles();

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
                            if (VehiclesFileExists())
                            {
                                searcher.VehicleListOfBrand();
                                wrongAction = false;
                            } else
                            {
                                Console.WriteLine("Files does not exist. Try again, please");
                                wrongAction = true;
                            }
                            break;
                        case "G":
                            if (VehiclesFileExists())
                            {
                                searcher.PredeterminedVehiclesList();
                                wrongAction = false;
                            }
                            else
                            {
                                Console.WriteLine("Files does not exist. Try again, please");
                                wrongAction = true;
                            }
                            break;
                        case "H":
                            if (VehiclesFileExists())
                            {
                                searcher.TotalValueOfVehicles();
                                wrongAction = false;
                            }
                            else
                            {
                                Console.WriteLine("Files does not exist. Try again, please");
                                wrongAction = true;
                            }
                            break;
                        case "I":
                            if (VehiclesFileExists())
                            {
                                searcher.VehicleListofBrandAndColor();
                                wrongAction = false;
                            }
                            else
                            {
                                Console.WriteLine("Files does not exist. Try again, please");
                                wrongAction = true;
                            }
                            break;
                        case "J":
                            if (VehiclesFileExists())
                            {
                                searcher.ServiceNeededVehiclesList();
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
            Console.ReadLine();
            return false;
        }

        static void DelateFiles()
        {
            foreach (FileInfo file in files)
            {
                if (file.Exists)
                    File.Delete(file.FullName);
            }
        }

        static bool VehiclesFileExists()
        {
            bool exist = false;
            for(int i=0; i<2; i++)
            {
                if (files[i].Exists)
                    exist = true;
            }
            return exist;
        }
    }
}
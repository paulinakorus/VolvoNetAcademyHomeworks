using Homework2.model;
using Homework2.service;
using System.Text;

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
                Console.WriteLine("\td\t-\tlist inventory of vehicles of specified brand");
                Console.WriteLine("\te\t-\tlist of predetermined vehicles of a chosen model");
                Console.WriteLine("\tf\t-\tcalculate total vehicle fleet value");
                Console.WriteLine("\tg\t-\tlist of vehicles of a chosen brand and color");
                Console.WriteLine("\th\t-\tlist of vehicles that are within 1000 km of requiring maintenance");

                bool wrongChar = true;
                while (wrongChar)
                {
                    Console.WriteLine("\nPlease enter the symbol of the function");
                    Console.Write("\tfunction: ");
                    char function = (char)Console.Read();
                    function = Char.ToUpper(function);

                    switch (function)
                    {
                        case 'A':
                            container.TypeVehicleAndAddToFile();
                            wrongChar = false;
                            break;
                        case 'B':
                            container.RemoveVehicle();
                            wrongChar = false;
                            break;
                        case 'C':
                            container.RentVehicle();
                            wrongChar = false;
                            break;
                        case 'D':
                            searcher.VehicleListOfBrand();
                            wrongChar = false;
                            break;
                        case 'E':
                            searcher.PredeterminedVehiclesList();
                            wrongChar = false;
                            break;
                        case 'F':
                            searcher.TotalValueOfVehicles();
                            wrongChar = false;
                            break;
                        case 'G':
                            searcher.VehicleListofBrandAndColor();
                            wrongChar = false;
                            break;
                        case 'H':
                            searcher.ServiceNeededVehiclesList();
                            wrongChar = false;
                            break;
                        default:
                            Console.WriteLine("Wrong symbol. Try again, please");
                            wrongChar = true;
                            break;
                    }
                }
                continuing = Answer();
                string waitingForAction = Console.ReadLine();
            }
            Console.WriteLine("The end of the program");
        }

        static bool Answer()
        {
            bool correct = false;
            while (!correct)
            {
                Console.WriteLine("Do you want to continue?");
                Console.Write("\tanswer (yes/no): ");

                var ans = Console.ReadLine();
                ans = ans.ToUpper();

                switch (ans)
                {
                    case "YES":
                        return true;
                    case "NO":
                        return false;
                    default:
                        Console.WriteLine("Incorrect answer");
                        Console.WriteLine("Try again\n");
                        correct = false;
                        break;
                }
            }
            return false;
        }
    }
}
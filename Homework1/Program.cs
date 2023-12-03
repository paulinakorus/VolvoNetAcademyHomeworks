namespace Homework1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            partOne();
        }

        static void partOne() {
            bool continuing = true;
            while (continuing)
            {
                Console.WriteLine("Homework 1a");
                Console.WriteLine("Calculating numbers");

                Console.WriteLine("\nPlease enter the numbers");
                Console.Write("\tx: ");
                var x = Console.ReadLine();
                Console.Write("\ty: ");
                var y = Console.ReadLine();

                Console.WriteLine("\nPlease enter the symbol of the operation");
                Console.WriteLine("\t+\t-\taddition");
                Console.WriteLine("\t-\t-\tsubtration");
                Console.WriteLine("\t*\t-\tmultiplication");
                Console.WriteLine("\t/\t-\tdivision");
                Console.WriteLine("\t^\t-\texponentiation");
                Console.WriteLine("\t!\t-\tfactorial");

                string result = TheOperation(x, y);
                if(result != null)
                    Console.WriteLine("result: " + result);

                continuing = Answer();
                Console.Clear();
            }
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

        static double Addition(string x, string y)
        {
            return Convert.ToDouble(x) + Convert.ToDouble(y);
        }

        static double Subtration(string x, string y)
        {
            return Convert.ToDouble(x) - Convert.ToDouble(y);
        } 

        static double Multiplication(string x, string y)
        {
            return Convert.ToDouble(x) * Convert.ToDouble(y);
        }

        static double Division(string x, string y)
        {
            return Convert.ToDouble(x) / Convert.ToDouble(y);
        }

        static double Exponentiation(string x, string y)
        {
            return Math.Pow(Convert.ToDouble(x), Convert.ToInt32(y));
        }

        static long Factorial (int x)
        {
            if (x == 0)
                return 1;
            else
            {
                return x * Factorial((x - 1));
            }
        }

        static string TheOperation(string x, string y)
        {
            bool correct = false;
            while (!correct)
            {
                Console.Write("\nthe operation: ");
                var operation = Console.ReadLine();

                switch (operation)
                {
                    case "+":
                        return Addition(x, y).ToString();
                    case "-":
                        return Subtration(x, y).ToString();
                    case "*":
                        return Multiplication(x, y).ToString();
                    case "/":
                        return Division(x, y).ToString();
                    case "^":
                        return Exponentiation(x, y).ToString();
                    case "!":
                        return Factorial(int.Parse(x)).ToString();
                    default:
                        Console.WriteLine("The symbol is incorrect");
                        Console.WriteLine("Try again\n");
                        correct = false;
                        break;
                }
            }
            return null;
        }
    }
}
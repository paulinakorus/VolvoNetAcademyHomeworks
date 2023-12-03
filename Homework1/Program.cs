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
                Console.Clear();
                Console.WriteLine("Homework 1a");
                Console.WriteLine("Calculating numbers");

                Console.WriteLine("\nPlease enter the numbers");
                Console.Write("\tx: ");
                var x = Console.ReadLine();
                Console.Write("\ty: ");
                var y = Console.ReadLine();

                //Console.WriteLine(ifDouble(x, y));
                if (!ifDouble(x, y))
                {
                    Console.WriteLine("Incorrect numbers\nTry again\n");
                    continuing = true;
                    Thread.Sleep(2000);
                }
                else
                {
                    Console.WriteLine("\nPlease enter the symbol of the operation");
                    Console.WriteLine("\t+\t-\taddition");
                    Console.WriteLine("\t-\t-\tsubtration");
                    Console.WriteLine("\t*\t-\tmultiplication");
                    Console.WriteLine("\t/\t-\tdivision");
                    Console.WriteLine("\t^\t-\texponentiation");
                    Console.WriteLine("\t!\t-\tfactorial");

                    string result = TheOperation(x, y);
                    if (result != null)
                    {
                        Console.WriteLine("result: " + result);
                        continuing = Answer();
                        Thread.Sleep(2000);
                    } 
                    else
                    {
                        continuing = true;
                        Console.WriteLine("Try again\n");
                        Thread.Sleep(2000);
                    }
                }
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

        static bool ifDivision(string x, string y)
        {
            if (Convert.ToInt32(y) != 0)
                return true;
            return false;
        }

        static bool ifFactorial(string x)
        {
            if (Convert.ToInt32(x) >= 0)
                return true;
            return false;
        }

        static bool ifDouble(string x, string y)
        { 
            var outX = 0.0;
            var outY = 0.0;
            var correctX = double.TryParse(x, out outX);
            var correctY = double.TryParse(y, out outY);

            if (correctX && correctY)
                return true;
            else
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
                bool correctNumber = true;

                switch (operation)
                {
                    case "+":
                        return Addition(x, y).ToString();
                    case "-":
                        return Subtration(x, y).ToString();
                    case "*":
                        return Multiplication(x, y).ToString();
                    case "/":
                        correctNumber = ifDivision(x, y);
                        if (correctNumber)
                        {
                            return Division(x, y).ToString();
                        }
                        Console.WriteLine("The number in the denominator is zero");
                        return null;
                    case "^":
                        return Exponentiation(x, y).ToString();
                    case "!":
                        correctNumber = ifFactorial(x);
                        if (correctNumber)
                        {
                            return Factorial(int.Parse(x)).ToString();
                        }
                        Console.WriteLine("The number in below zero");
                        return null;
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
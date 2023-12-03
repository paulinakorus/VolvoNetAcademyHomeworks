namespace Homework1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Homework 1");
            Console.WriteLine("Calculating numbers");

            partOne();
        }

        static void partOne() {
            bool continuing = true;
            while (continuing)
            {
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

                Console.Write("\nthe operation: ");
                var operation = Console.ReadLine();
                string result;
                TheOperation(x, y, operation, out result);
            }
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

        static void TheOperation (string x, string y, string operation, out string result)
        {
            switch (operation)
            {
                case "+":
                    result = Addition(x, y).ToString();
                    Console.WriteLine(result);
                    break;
                case "-":
                    result = Subtration(x, y).ToString();
                    Console.WriteLine(result);
                    break;
                case "*":
                    result = Multiplication(x, y).ToString();
                    Console.WriteLine(result);
                    break;
                case "/":
                    result = Division(x, y).ToString();
                    Console.WriteLine(result);
                    break;
                case "^":
                    result = Exponentiation(x, y).ToString();
                    Console.WriteLine(result);
                    break;
                case "!":
                    result = Factorial(int.Parse(x)).ToString();
                    Console.WriteLine(result);
                    break;
                default: 
                    result = null;
                    Console.WriteLine("The symbol is incorrect");
                    Console.WriteLine("Try again\n");
                    break;
            }
        }
    }
}
namespace Homework1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            
        }

        static double Addition(double x, double y)
        {
            return x + y;
        }

        static double Subtration(double x, double y)
        {
            return x - y;
        } 

        static double Multiplication(double x, double y)
        {
            return x * (double)y;
        }

        static double Division(double x, double y)
        {
            return x / (double)y;
        }

        static double Exponentiation (double x, int y)
        {
            return Math.Pow(x, y);
        }

        static long Factorial (int x)
        {
            if (x == 0)
                return 1;
            else
            {
                return x * Factorial(x - 1);
            }
        } 

    }
}
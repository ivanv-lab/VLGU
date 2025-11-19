namespace Lab_3_1_12;

class Program
{
    static void Main(string[] args)
    {
        double a = 3.1;
        double b = 0.1;
        for (double x = 0; x <= 5; x += 0.5)
        {
            if (x < 3)
            {
                Console.WriteLine("y = "+Math.Pow(Math.E,x)+Math.Cos(a*x));
            }

            if (x == 3)
            {
                Console.WriteLine("y = "+(a+b)/(x+1));
            }

            if (x > 3)
            {
                Console.WriteLine("y = "+Math.Pow(Math.E,x)+Math.Sin(b*x));
            }
        }

        Console.ReadKey();
    }
}
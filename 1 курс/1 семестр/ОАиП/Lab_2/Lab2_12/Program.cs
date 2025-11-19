namespace Lab2_12
{
    internal class Program
    {
        static void Main(string[] args)
        {
            try
            {
                Console.WriteLine("Введите число x");
                double x = Convert.ToDouble(Console.ReadLine());
                Console.WriteLine("Введите число y");
                double y = Convert.ToDouble(Console.ReadLine());
                Console.WriteLine("Введите число z");
                double z = Convert.ToDouble(Console.ReadLine());

                double v = -1;
                if (y > x && y > z) v = 1;
                if (y < x && y < z) v = 0;

                Console.WriteLine("V = " + v);

                Console.ReadKey();
            }
            catch
            {
                Console.WriteLine("Введите корректное число");
            }
        }
    }
}

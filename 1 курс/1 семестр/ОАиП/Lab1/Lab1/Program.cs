namespace Lab1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            try
            {
                Console.WriteLine("Вычисление синуса числа n");
                Console.WriteLine("Введите число n");
                double n = Convert.ToDouble(Console.ReadLine());
                Console.WriteLine("Синус числа " + n + " = " + Math.Sin(n));

                Console.ReadKey();
            }
            catch
            {
                Console.WriteLine("Введите корректное число");
            }
        }
    }
}

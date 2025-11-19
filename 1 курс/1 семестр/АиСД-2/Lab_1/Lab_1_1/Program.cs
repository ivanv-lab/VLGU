namespace Lab_1_1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            try
            {
                Console.WriteLine("Числа Фибоначчи");
                Console.WriteLine("Введите n:");
                int n=Convert.ToInt32(Console.ReadLine());
                Console.WriteLine("Ряд чисел Фибоначчи:");
                for (int i = 0; i < n; i++)
                {
                    Console.WriteLine(Fibon(i));
                }


            }
            catch
            {
                Console.WriteLine("Введите корректное число");
            }
        }

        public static int Fibon(int n)
        {
            if (n < 2)
                return n;
            else
                return Fibon(n - 1) + Fibon(n - 2);
        }

    }
}

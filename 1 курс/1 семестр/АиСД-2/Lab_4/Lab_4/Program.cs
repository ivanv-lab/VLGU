namespace Lab_4
{
    internal class Program
    {
        static void Main(string[] args)
        {
            try
            {
                Console.WriteLine("Введите n");
                int n=Convert.ToInt32(Console.ReadLine());
                var result=Count(n);
                Console.WriteLine("Кол-во последовательностей = " + result);

                Console.ReadKey();
                    }
            catch
            {
                Console.WriteLine("Введите корректное число");
            }
        }

        public static dynamic Count(dynamic n)
        {
            if (n <= 0)
                return 0;
            else if (n == 1)
                return 2;
            else if (n == 2)
                return 3;
            else
            {
                dynamic a = 2;
                dynamic b = 3;
                for(int i = 3; i < n + 1; i++)
                {
                    a = b;
                    b = a + b;
                }
                return b;
            }
        }
    }
}

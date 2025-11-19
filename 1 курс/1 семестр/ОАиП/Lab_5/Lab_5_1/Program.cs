namespace Lab_5_1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int[,] c = new int[5,5];
            int q = 0;

            Random random = new Random();
            Console.WriteLine("Матрица:");
            for (int i = 0; i < 5; i++)
            {
                for (int j = 0; j < 5; j++)
                {
                    c[i,j]=random.Next(-100,100);
                    Console.Write(c[i,j]+" | ");

                    if (i == j)
                    {
                        if (c[i,j] % 3 == 0)
                        {
                            q++;
                        }
                    }
                }
                Console.WriteLine();
            }

            Console.WriteLine("Кол-во значений на главной диагонали " +
                "кратных трём = "+q);

            Console.ReadKey();
        }
    }
}

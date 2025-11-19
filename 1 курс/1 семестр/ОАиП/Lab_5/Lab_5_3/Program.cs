namespace Lab_5_3
{
    internal class Program
    {
        static void Main(string[] args)
        {
            try
            {
                Console.WriteLine("Введите M:");
                int m = Convert.ToInt32(Console.ReadLine());
                Console.WriteLine("Введите N:");
                int n=Convert.ToInt32(Console.ReadLine());

                int[,] b = new int[m, n];
                Random random = new Random();
                for (int i = 0; i < m; i++)
                {
                    for (int j = 0; j < n; j++)
                    {
                        b[i, j] = random.Next(-100, 100);
                        Console.Write(b[i,j]+" | ");
                    }
                    Console.WriteLine();
                }

                int[] mas = new int[n];
                int s = 0;

                for(int i = 0;i<n; i++)
                {
                    for(int j = 0;j<m; j++)
                    {
                        if (b[j,i] % 2 != 0)
                        {
                            s += b[j, i];
                        }
                    }
                    mas[i] = s;
                    s = 0;
                }

                Console.WriteLine("Массив из сумм элементов c нечетными значениями каждого столбца матрицы: ");
                foreach (int i in mas)
                {
                    Console.WriteLine(i);
                }

                Console.ReadKey();
            }
            catch
            {
                Console.WriteLine("Введите корректное число");
            }
        }
    }
}

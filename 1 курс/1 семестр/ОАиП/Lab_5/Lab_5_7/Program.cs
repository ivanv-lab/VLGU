namespace Lab_5_7
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
                int n = Convert.ToInt32(Console.ReadLine());

                int[,] c = new int[m, n];
                int q = 0;
                int s = 0;
                Random random = new Random();
                for (int i = 0; i < m; i++)
                {
                    for (int j = 0; j < n; j++)
                    {
                        c[i, j] = random.Next(-100, 100);
                        Console.Write(c[i, j] + " | ");

                        s += c[i, j];
                    }
                    Console.WriteLine();
                    if (s > 0)
                    {
                        q += n;
                        s = 0;
                    }
                    s = 0;
                }

                int[] v = new int[q];
                int a = 0;
                int w = 0;
                int e = 0;

                for(int i = 0; i < m; i++)
                {
                    for(int j = 0;j < n; j++)
                    {
                        s += c[i, j];
                    }
                    if (s > 0)
                    {
                        for(w = a; w < n+a; w++)
                        {
                            v[w] = c[i, e];
                            e++;
                        }
                    }
                    s = 0;
                    a = w;
                    e = 0;
                }


                Console.WriteLine("Сформированный массив:");
                foreach(int i in v)
                {
                    Console.WriteLine(i);
                }

                Console.ReadKey();
            }
            catch {
                Console.WriteLine("Введите корректное число");
            }
        }
    }
}

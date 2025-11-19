namespace Lab_4_5
{
    internal class Program
    {
        static void Main(string[] args)
        {
            try
            {
                Console.WriteLine("Введите кол-во элементов в массиве Z");
                int n = Convert.ToInt32(Console.ReadLine());

                double[] z = new double[n];
                int p = 0, o = 0;

                for (int i = 0; i < n; i++)
                {
                    Console.WriteLine("Введите " + (i + 1) + "-й элемент массива");
                    z[i] = Convert.ToDouble(Console.ReadLine());
                    if (z[i] >= 0) p++;
                    if (z[i] < 0) o++;
                }

                Console.WriteLine("Исходный массив Z:");
                foreach (double i in z)
                {
                    Console.WriteLine(i);
                }

                double[] pol = new double[p];
                double[] otr = new double[o];
                int w = 0, q = 0;

                foreach (int i in z)
                {
                    if (i >= 0)
                    {
                        pol[w] = i;
                        w++;
                    }
                    if (i < 0)
                    {
                        otr[q] = i;
                        q++;
                    }
                }

                double[] r = new double[n];
                for (int i = 0; i < pol.Length; i++)
                {
                    r[i] = pol[i];
                }

                int t = 0;
                for (int i = pol.Length; i < n; i++)
                {
                    r[i] = otr[t];
                    t++;
                }

                Console.WriteLine("Изменененный массив R:");
                foreach (int i in r)
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

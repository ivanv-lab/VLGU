namespace Lab4_1;

class Program
{
    static void Main(string[] args)
    {
        try
        {
            Console.WriteLine("Введите кол-во элементов вектора");
            int l = Convert.ToInt32(Console.ReadLine());

            double[] mas = new double [l];
            for (int i = 0; i < l; i++)
            {
                Console.WriteLine("Введите "+(i+1)+"-й элмент вектора");
                mas[i] = Convert.ToDouble(Console.ReadLine());
            }

            Console.WriteLine("Введите отрезок [x,y]:");
            Console.WriteLine("Введите x");
            int x = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Введите y");
            int y = Convert.ToInt32(Console.ReadLine());

            double p = 1;
            Console.WriteLine("Вектор A:");
            foreach (int i in mas)
            {
                Console.WriteLine(i);
            }

            for (int i = x; i <= y; i++)
            {
                p *= mas[i];
            }

            Console.WriteLine();
            Console.WriteLine("P = "+p);

            Console.ReadKey();
        }
        catch
        {
            Console.WriteLine("Введите корректное число");
        }
    }
}
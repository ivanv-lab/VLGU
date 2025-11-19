namespace Lab4_3;

class Program
{
    static void Main(string[] args)
    {
        try
        {
            Console.WriteLine("Введите кол-во элементов вектора");
            int n = Convert.ToInt32(Console.ReadLine());

            double[] mas = new double[n];

            for (int i = 0; i < n; i++)
            {
                Console.WriteLine("Введите " + (i + 1) + "-й элемент вектора");
                mas[i] = Convert.ToDouble(Console.ReadLine());
            }

            Console.WriteLine("Исходный вектор:");
            foreach (int i in mas)
            {
                Console.WriteLine(i);
            }

            Console.WriteLine("Измененный вектор:");
            for (int i = 0; i < n; i++)
            {
                if (mas[i] > 0) mas[i] = mas[i]-0.7;
                else
                if (mas[i] < 0) mas[i] = Math.Abs(mas[i]);

                Console.WriteLine(mas[i]);
            }

            for (int i = 0; i < n; i++)
            {
                if (mas[i] < 0)
                {
                    Console.WriteLine("Номер первого отрицательного элемента:");
                    Console.WriteLine(i+1);
                    return;
                }
            }

            Console.ReadKey();
        }
        catch
        {
            Console.WriteLine("Введите корректное число");
        }
    }
}
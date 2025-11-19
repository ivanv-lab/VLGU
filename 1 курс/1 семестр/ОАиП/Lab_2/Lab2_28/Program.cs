namespace Lab2_28
{
    internal class Program
    {
        static void Main(string[] args)
        {
            try
            {
                Console.WriteLine("1 - радиус");
                Console.WriteLine("2 - диаметр");
                Console.WriteLine("3 - длина окружности");
                Console.WriteLine("Выберите способ вычисления площади круга");

                int n = Convert.ToInt32(Console.ReadLine());

                switch (n)
                {
                    case 1:
                        {
                            Console.WriteLine("Вычисление по радиусу (номер 1)");
                            Console.WriteLine("Введите радиус");
                            double r = Convert.ToDouble(Console.ReadLine());
                            Console.WriteLine("Площадь круга S = " + Math.PI * r * r);

                            break;
                        }
                    case 2:
                        {
                            Console.WriteLine("Вычисление по диаметру (номер 2)");
                            Console.WriteLine("Введите диаметр");
                            double d=Convert.ToDouble(Console.ReadLine());
                            Console.WriteLine("Площадь круга S = " + (Math.PI * d * d) / 4);

                            break;
                        }
                    case 3:
                        {
                            Console.WriteLine("Вычисление по длине окружности (номер 3)");
                            Console.WriteLine("Введите длину окружности");
                            double o=Convert.ToDouble(Console.ReadLine());
                            Console.WriteLine("Площадь круга S = "+(o*o)/4*Math.PI);

                            break;
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
}

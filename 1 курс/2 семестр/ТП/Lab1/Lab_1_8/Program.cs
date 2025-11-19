using System.Threading.Channels;

namespace Lab_1_8
{
    internal class Program
    {
        static void Main(string[] args)
        {
            try
            {
                Console.WriteLine("Введите кол-во элементов массива:");
                int arraySize=Int32.Parse(Console.ReadLine());

                int[]binaryArray=new int[arraySize];
                for(int i = 0; i < binaryArray.Length; i++)
                {
                    Console.Write("Введите " + i.ToString() + " элемент ");
                    binaryArray[i] = Int32.Parse(Console.ReadLine());
                    if(binaryArray[i] !=1 && binaryArray[i] != 0)
                    {
                        Console.WriteLine("Введите корректные данные");
                        return;
                    }
                }

                double n = 0;
                for(int i = 0; i < binaryArray.Length; i++)
                {
                    n += binaryArray[i] * Math.Pow(2, binaryArray.Length - 1 - i);
                }

                Console.WriteLine("Десятичное представление числа: " + n);
                Console.ReadKey();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Введите корректные данные");
                Console.WriteLine(ex.ToString());
            }
        }
    }
}

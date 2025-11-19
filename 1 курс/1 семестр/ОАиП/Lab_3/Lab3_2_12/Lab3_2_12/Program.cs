namespace Lab3_2_12;

class Program
{
    static void Main(string[] args)
    {
        try
        {
            Console.WriteLine("Введите целое число n");
            int n = Convert.ToInt32(Console.ReadLine());

            double s=0;
            
            for(int i=1;i<=n;i++)
            {
                s = Math.Sqrt(2+s);
            }

            Console.WriteLine(s);

            Console.ReadKey();
        }
        catch
        {
            Console.WriteLine("Введите корректное число");
        }
    }
}
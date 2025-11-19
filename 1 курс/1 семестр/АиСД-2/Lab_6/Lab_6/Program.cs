namespace Lab_6
{
    internal class Program
    {
        static void Main(string[] args)
        {
            List<string> lines = new List<string>();

            lines = File.ReadAllLines("f0.txt")
                .ToList();

            Console.WriteLine("Строки в файле и кол-во элементов");
            foreach (string line in lines)
            {
                Console.WriteLine(line+" | "+line.Length);
            }

            Console.ReadLine();
        }
    }
}

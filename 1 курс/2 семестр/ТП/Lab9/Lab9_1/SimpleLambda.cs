namespace Lab9_1
{
    internal class SimpleLambda
    {
        delegate Int32 simpleDelegate(Int32 i);
        static void Main(string[] args)
        {
            simpleDelegate Square = x => x * x;
            int squareRes = Square(7);
            Console.WriteLine($"Результат: {squareRes}");
        }
    }
}

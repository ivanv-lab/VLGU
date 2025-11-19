namespace Lab9_2
{
    internal class LambdaTest
    {
        static void Main(string[] args)
        {
            List<Man> mansCollection = new List<Man>()
            {
                new Man(15, "Петя"),
                new Man(45, "Вася"),
                new Man(13, "Оля"),
                new Man(29, "Катя"),
                new Man(5, "Ваня"),
                new Man(54, "Боб"),
                new Man(22, "Алиса")
            };

            List<Man> filteredMans = mansCollection
                .FindAll(mansCollection => mansCollection.Age > 16);

            filteredMans.ForEach((m) => 
            Console.WriteLine(m.Name + " " + m.Age));
        }
    }
}

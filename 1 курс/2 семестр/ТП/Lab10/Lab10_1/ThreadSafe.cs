namespace Lab10_1
{
    internal class ThreadSafe
    {
        static int maxi = 30;
        static int i;
        static object locker=new object();
        static void Main(string[] args)
        {
            new Thread(GoA).Start();
            GoB();
            Console.ReadLine();
        }

        static void GoA()
        {
            for (; i < maxi;)
            {
                lock (locker)
                {
                    Console.Write("A" + i + " ");
                }
                i++;
            }
        }

        static void GoB()
        {
            for (; i < maxi;)
                lock (locker)
                {
                    Console.Write("B" + i + " ");
                }
            i++;
        }
    }
}

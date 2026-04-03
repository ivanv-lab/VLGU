using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace Lab7
{
    public class MainClass
    {
        public static void Main(string[] args)
        {
            long numPoints = 100_000_000L;
            long pointsInsideCircle = 0;

            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();

            for (int i = 0; i < numPoints; i++)
            {
                double x = new Random().NextDouble();
                double y = new Random().NextDouble();
                if (x * x + y * y <= 1)
                    pointsInsideCircle++;
            }

            double pi = 4.0 * pointsInsideCircle / numPoints;
            numPoints = 100_000_000L;
            pointsInsideCircle = 0;

            stopwatch.Stop();
            Console.WriteLine($"PI = {pi}");
            Console.WriteLine("Затраченное время в однопоточном выполении: " + stopwatch.Elapsed
                .TotalMilliseconds + "мс");

            object locker = new object();

            stopwatch.Restart();
            Parallel.For(0, numPoints, (i) =>
            {
                Random random = new Random(Guid.NewGuid().GetHashCode());
                double x = random.NextDouble();
                double y = random.NextDouble();
                long localPointsInsideCircle = 0;

                if (x * x + y * y <= 1)
                {
                    lock (locker)
                    {
                        pointsInsideCircle++;
                    }
                }
            });

            pi = 4.0 * pointsInsideCircle / numPoints;

            stopwatch.Stop();
            Console.WriteLine($"PI = {pi}");
            Console.WriteLine("Затраченное время в многопоточном выполнении: " + stopwatch.Elapsed
                .TotalMilliseconds + "мс");
            Console.ReadLine();
        }
    }
}

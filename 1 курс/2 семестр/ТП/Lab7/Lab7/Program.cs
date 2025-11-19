using Lab6_1;
using static Lab7.SessionEvent;

namespace Lab7
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello, World!");

            Group ist110 = new Group("IST110", 10);
            ist110.InitRandom();

            SessionEvent sessionEvent=new SessionEvent();

            sessionEvent.activate += new
                RatingHandler(StudentGroupActions.PrintRatingVedomost);
            sessionEvent.activate += new
                RatingHandler(StudentGroupActions.CalculateRatings);
            sessionEvent.activate += new
                RatingHandler(StudentGroupActions.PrintRankingList);
            sessionEvent.activate += new 
                RatingHandler(Emailer.SendSessionNotify);

            sessionEvent.FixSemesterResults(ist110);

            sessionEvent.activate -= new
                RatingHandler(StudentGroupActions.PrintRatingVedomost);

            Console.ReadLine();
        }
    }
}

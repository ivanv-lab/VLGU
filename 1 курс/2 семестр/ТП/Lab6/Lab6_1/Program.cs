namespace Lab6_1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello, World!");

            Group ist110 = new Group("IST110", 10);
            ist110.InitRandom();

            StudentRatingProcessor studentRating;

            StudentRatingProcessor Print = StudentGroupActions.PrintRatingVedomost;
            StudentRatingProcessor Calculate = StudentGroupActions.CalculateRatings;
            StudentRatingProcessor PrintList = StudentGroupActions.PrintRankingList;

            studentRating = Print;
            studentRating += Calculate;
            studentRating += PrintList;

            studentRating(ist110);

            Console.ReadLine();
        }
    }
}

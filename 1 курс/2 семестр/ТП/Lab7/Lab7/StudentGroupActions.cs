namespace Lab6_1
{
    public delegate void StudentRatingProcessor(Group group);
    enum RatingStatus { Passed,Notpassed};
    class StudentGroupActions
    {

        //печать рейтинговой ведомости 
        public static void PrintRatingVedomost(Group group)
        {
            Console.WriteLine("----Рейтинговая ведомость----");
            Console.WriteLine(group.GroupTitle);
            Console.WriteLine("Математика");
            Console.WriteLine("Технологии программирования");
            Console.WriteLine("Базы данных");
            Console.WriteLine("Алгоритмизация");
            Console.WriteLine("---- ----");
        }

        //расчет рейтинга студентов 
        public static void CalculateRatings(Group group)
        {
            for (int i = 0; i < group.GroupSize; i++)
            {
                
                int summBalls = 0;
                for(int j = 0; j < 4; j++)
                {
                    summBalls += group[i][j];
                }
                 if (summBalls >= (10 * 4 / 2)) group[i].Rating = (int)RatingStatus.Passed;
                 else group[i].Rating = (int)RatingStatus.Notpassed;
            }
        }

        //печать зачетной ведомости 
        public static void PrintRankingList(Group group)
        {
            for(int i=0; i < group.GroupSize;i++)
            Console.WriteLine("Студент: " + group[i].FIO+" Результат: "+((group[i].Rating == (int)RatingStatus.Passed) ?
    "ЗАЧТЕНО" : "НЕЗАЧТЕНО"));
        }
    }
}

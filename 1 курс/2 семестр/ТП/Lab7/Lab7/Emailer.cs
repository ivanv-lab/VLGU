using Lab6_1;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab7
{
    public class Emailer
    {
        public static void SendSessionNotify(Group group)
        {
            //отправить e-mail уведомление группе с рейтинговой / зачетной ведомостью 
            Console.WriteLine("На email группы " + group.GroupTitle + 
                " отправлена рейтинговая ведомость..."); 
        }
    }
}

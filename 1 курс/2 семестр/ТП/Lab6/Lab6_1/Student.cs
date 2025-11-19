using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab6_1
{
    public class Student
    {
        String fio;
        public String FIO { set { fio = value; } get { return fio; } }

        Byte[] balls;
        public Byte this[int i] { get { return balls[i]; } set { balls[i] = value; } }

        int rating;
        public int Rating { get { return rating; } set { rating = value; } }

        public Student(String FIO)
        {
            rating = 0;
            balls = new Byte[4];
            fio = FIO;
        }
    }
}

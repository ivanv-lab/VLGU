using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab6_1
{
    public class Group
    {
        int groupSize;
        string groupTitle = "";

        public string GroupTitle { get { return groupTitle; } }
        public int GroupSize { get { return groupSize; } }

        Student[] students;
        public Student this[int i] { get { return students[i]; } }

        public Group(string Title, int Size)
        {
            groupTitle = Title;
            groupSize = Size;
            students = new Student[Size];
        }

        public void InitRandom()
        {
            try
            {
                var rnd = new Random();
                for (int i = 0; i < GroupSize; i++)
                {
                    String randomString = "";
                    for (int j = 0; j < 5; j++)
                        randomString += (rnd.Next(255).ToString());
                    students[i] = new Student(randomString);

                    for (int j = 0; j < 4; j++) students[i][j] = (byte)rnd.Next(10);
                }
            }
            catch (System.ArgumentOutOfRangeException e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}

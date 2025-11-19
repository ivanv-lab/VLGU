using Lab6_1;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab7
{

    public class SessionEvent
    {
        public delegate void RatingHandler(Group group);
        public event RatingHandler? activate;
        public void FixSemesterResults(Group group)
        {
            if(activate != null) activate(group);
        }
    }
}

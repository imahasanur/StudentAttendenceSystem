using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentAttendence
{
    public class StudentsAttendences
    {
        public int ScheduleId { get; set; }
        public string StudentUserName { get; set; }

        //public StudentsCourses StudentsCourses { get; set; }
        public ClassSchedules Classes { get; set; }

    }
}

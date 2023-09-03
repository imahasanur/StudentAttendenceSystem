using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentAttendence
{
    public class ClassSchedules
    {
        public int Id { get; set; }
        public int CourseId { get; set; }
        public DateTime Date { get; set; }
        public string Day { get; set; }
        public string StartTime { get; set; }
        public string EndTime { get; set; }
        public int TotalClass { get; set; }
        public Courses Courses { get; set; }
        public List<StudentsAttendences> AttendClasses { get; set; }

    }
}

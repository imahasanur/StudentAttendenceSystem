using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentAttendence
{
    public class StudentsCourses
    {
        public int CourseId { get; set; }
        public string StudentUserName { get; set;}
        public Users Students { get; set; }
        public Courses AssignedCourses { get; set; }
       // public List<StudentsAttendences> CoursesList { get; set; }
    }
}

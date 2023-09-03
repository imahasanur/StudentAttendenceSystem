using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentAttendence
{
    public class Courses
    {
        public int CourseId { get; set; }
        public string CourseName { get; set; }
        public int Fees { get; set; }
        
        public List<TeachersCourse> TeacherCourse { get; set; }
        public List<StudentsCourses> StudentsCourses { get; set; }
        public List<ClassSchedules> CourseClasses { get; set; }

        //public string? StudentUserName { get; set; }
        //public string? TeacherUserName { get; set; }
        //public string Type { get; set; }
        //public Users Teacher { get; set; }
        //public Users Student { get; set; }

    }
}

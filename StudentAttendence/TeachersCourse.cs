using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentAttendence
{
    public class TeachersCourse
    {
        public int CourseId { get; set; }
        public string TeacherUserName { get; set; }
        public Users Teacher{ get; set; }
        public Courses AssignedCourse { get; set; }
    }
}

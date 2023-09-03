using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentAttendence
{
    public class Users
    {
        public string Name { get; set; }
        public string UserName { get; set; }
        public string UserPassword { get; set; }
        public string Type { get; set; }
        public List<TeachersCourse> GivenTeacherCourse { get; set; }  
        public List<StudentsCourses> GivenStudentsCourses { get; set; }
    }
}

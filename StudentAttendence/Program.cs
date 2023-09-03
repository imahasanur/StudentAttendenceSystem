using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.Identity.Client;
using StudentAttendence;
using System;
using System.Collections;
using System.Security.Cryptography.X509Certificates;

ProjectDBContext context = new ProjectDBContext();


string userName;
string password;
bool isRunning = true;
bool isLogIn = true;

//Starting point of the system
while (isRunning)
{

    //Login Part
    Console.WriteLine("Please Enter your valid user name & password :: ");
    Console.WriteLine("Enter Your username :: ");
    userName = Console.ReadLine();
    Console.WriteLine("Enter Your Password :: ");
    password = Console.ReadLine();
    
    //DB call for user table user checking
    Users users = context.Users.Where(x => (x.UserName == userName && x.UserPassword == password)).FirstOrDefault();

    if (users == null)
    {
        Console.WriteLine("Your UserName or Password is Incorrect !! Try again ..");
        continue;
    }
    else
    {
        isLogIn = true;
    }

    //After login to the system
    while (isLogIn)
    {
        string loggedInUser = users.UserName;
        string type = users.Type;

        //When User is admin
        while (type == "admin" && isLogIn)
        {
            Console.WriteLine($" Hey {loggedInUser} , Your are Logged In as {type} ");
            string num;
            int count;

            //for including teacher, student or course part 
            while (isLogIn)
            {

                Console.WriteLine("Type 1 for Teacher including .");
                Console.WriteLine("Type 2 for Students including .");
                Console.WriteLine("Type 3 for Course including .");
                Console.WriteLine("Type 4 for log out .");
                Console.WriteLine("Type 5 for Skip and Next step .");
                num = Console.ReadLine();
                //Console.WriteLine($"Input value {num}");

                if (num == "4")
                {
                    isLogIn = false;
                    break;
                }
                else if(num == "5")
                {
                    break;
                }


                if (num != "1" && num != "2" && num != "3")
                {
                    Console.WriteLine("Please Enter 1 or 2 or 3");
                    continue;
                }

                Console.WriteLine("Enter number of Entry You want to include :: ");
                count = Convert.ToInt32(Console.ReadLine());

                //for adding user teacher or student
                if (num == "1" || num == "2")
                {
                    List<Users> entry = new();
                    for (int i = 0; i < count; i++)
                    {
                        Console.WriteLine($"Entry No {i + 1} out of {count}");

                        string name, userNam, pass, typo = "";
                        Console.WriteLine("Enter Full Name ");
                        name = Console.ReadLine();
                        Console.WriteLine("Enter User Name");
                        userNam = Console.ReadLine();
                        Console.WriteLine("Enter Password");
                        pass = Console.ReadLine();

                        if (num == "1")
                            typo = "teacher";
                        else if (num == "2")
                            typo = "student";

                        entry.Add(new Users { Name = name, UserName = userNam, UserPassword = pass, Type = typo });
                    }
                    try
                    {
                        context.Users.AddRange(entry);
                        context.SaveChanges();
                    }
                    catch(Exception e)
                    {
                        Console.WriteLine($" Error is -> {e.Message} ");
                        Console.WriteLine("You may giving input User Name which is already used");
                        context.ChangeTracker.Clear();
                    }


                    //context.Users.AddRange(entry);
                    //context.SaveChanges();
                }
                else
                {
                    //for adding course
                    List<Courses> entry = new();

                    for (int i = 0; i < count; i++)
                    {

                        string courseName;
                        int fees;
                        try
                        {
                            Console.WriteLine($"Entry No {i + 1} out of {count}");

                            Console.WriteLine("Enter Course Name ");
                            courseName = Console.ReadLine();
                            Console.WriteLine("Enter Course Fees (integer) ");
                            fees = Convert.ToInt32(Console.ReadLine());

                            entry.Add(new Courses { CourseName = courseName, Fees = fees });
                        }
                        catch(Exception e)
                        {
                            Console.WriteLine($"Error is -> {e} ");
                            Console.WriteLine("You may entered course fees in different data type ! type in integer .");
                            
                        }

                    }
                    try
                    {
                        context.Courses.AddRange(entry);
                        context.SaveChanges();
                    }
                    catch(Exception e)
                    {
                        Console.WriteLine($"Error is -> {e}");
                        context.ChangeTracker.Clear();
                    }

                }

                Console.WriteLine("Want to add More Teacher or Student or Course Entry Type 1 (yes) or 2 (no) ");
                var wish = Console.ReadLine();
                if (wish == "1" || wish == "yes")
                    continue;
                else
                {
                    break;
                }
            }

            //if selected logout option 
            if (!isLogIn)
            {
                break;
            }

            //for assigining student and teacher course assigning part
            while (isLogIn)
            {

                Console.WriteLine("Type 1 for Teacher Course Assigning .");
                Console.WriteLine("Type 2 for Students Course Assigning .");
                Console.WriteLine("Type 3 for log out .");
                Console.WriteLine("Type 4 for skip the step");
                num = Console.ReadLine();

                if (num == "3")
                {
                    isLogIn = false;
                    break;
                }
                else if(num == "4")
                {
                    break;
                }

                if (num != "1" && num != "2")
                {
                    Console.WriteLine("Please Enter 1 or 2");
                    continue;
                }

                Console.WriteLine("Enter number of Entry You want to include :: ");
                count = Convert.ToInt32(Console.ReadLine());


                //for teacher course assign
                if (num == "1")
                {
                    List<TeachersCourse> entry = new();
                    for (int i = 0; i < count; i++)
                    {
                        Console.WriteLine($"Entry No {i + 1} out of {count}");

                        string teacherUserName;
                        int courseId;

                        Console.WriteLine("Enter Teacher User Name ");
                        teacherUserName = Console.ReadLine();
                        Console.WriteLine("Enter Assigned Course Id");
                        courseId = Convert.ToInt32(Console.ReadLine());


                        entry.Add(new TeachersCourse { CourseId = courseId, TeacherUserName = teacherUserName });
                    }
                    //context.TeachersCourses.AddRange(entry);
                    //context.SaveChanges();
                    try
                    {
                        context.TeachersCourses.AddRange(entry);
                        context.SaveChanges();
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine($"Error is -> {e.Message}");
                        context.ChangeTracker.Clear();
                    }

                }
                else
                {
                    //for student course assign

                    List<StudentsCourses>entry = new();
                    for (int i = 0; i < count; i++)
                    {
                        Console.WriteLine($"Entry No {i + 1} out of {count}");
                        string studentUserName;
                        int courseId;

                        Console.WriteLine("Enter Student User Name ");
                        studentUserName = Console.ReadLine();
                        Console.WriteLine("Enter Assigned Course Id");
                        courseId = Convert.ToInt32(Console.ReadLine());

                        entry.Add(new StudentsCourses { CourseId = courseId, StudentUserName = studentUserName });
                    }
                    //context.StudentsCourses.AddRange(entry);
                    //context.SaveChanges();
                    try
                    {
                        context.StudentsCourses.AddRange(entry);
                        context.SaveChanges();
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine($"Error is -> {e}");
                        context.ChangeTracker.Clear();
                    }

                }

                Console.WriteLine("Want to assign More Teacher's or Student's Course Entry Type 1 (yes) or 2 (no) ");
                var wish = Console.ReadLine();
                if (wish == "1" || wish == "yes")
                    continue;
                else
                {
                    break;
                }

            }
            if (!isLogIn)
            {
                break;
            }

            //for assigining course schedule part
            while (isLogIn)
            {

                Console.WriteLine("Type 1 for Course Time Scheduling .");
                Console.WriteLine("Type 2 for log out .");
                Console.WriteLine("Type 3 for skip the step .");

                num = Console.ReadLine();

                if (num == "2")
                {
                    isLogIn = false;
                    break;
                }
                else if(num == "3")
                {
                    break;
                }


                if (num != "1")
                {
                    Console.WriteLine("Please Enter 1");
                    continue;
                }

                Console.WriteLine("Enter number of Entry You want to include :: ");
                count = Convert.ToInt32(Console.ReadLine());


                //for course class schedule entry
                if (num == "1")
                {
                    List<ClassSchedules> entry = new();
                    DateTime aDate = DateTime.Now;
                    int aDay = (int)aDate.DayOfWeek;
                    for (int i = 0; i < count; i++)
                    {
                        Console.WriteLine($"Entry No {i + 1} out of {count}");

                        string day, startTime, endTime;
                        int courseId, totalClass, dayNum;
                        DateTime date;

                        Console.WriteLine("Enter Assigned Course Id");
                        courseId = Convert.ToInt32(Console.ReadLine());

                        Console.WriteLine("Enter day number of the week ");
                        Console.WriteLine(" 0 - Sunday \n 1 - Monday \n 2 - Tuesday \n 3 - Wednesday \n 4 - Thursday \n 5 - Friday \n 6 - Saturday ");
                        dayNum = Convert.ToInt32(Console.ReadLine());
                        DateTime newDate = aDate.AddDays(Math.Abs(aDay - dayNum));

                        Console.WriteLine("Enter Class Start Time Format (08:00 AM)");
                        startTime = Console.ReadLine();
                        Console.WriteLine("Enter Class End Time Format (08:00 PM)");
                        endTime = Console.ReadLine();

                        Console.WriteLine("Enter total class ");
                        totalClass = Convert.ToInt32(Console.ReadLine());
                        for (int k = 0; k < totalClass; k++)
                        {

                            entry.Add(new ClassSchedules { CourseId = courseId, Date = newDate, Day = newDate.DayOfWeek.ToString(), StartTime = startTime, EndTime = endTime, TotalClass = totalClass });
                            newDate = newDate.AddDays(7);
                        }


                    }
                    try
                    {
                        context.ClassSchedules.AddRange(entry);
                        context.SaveChanges();
                    }
                    catch(Exception e)
                    {
                        Console.WriteLine($"Error is ->{e}");
                        context.ChangeTracker.Clear();
                    }
                    //context.ClassSchedules.AddRange(entry);
                    //context.SaveChanges();
                }

                Console.WriteLine("Want to add more course class schedule Entry Type 1 (yes) or 2 (no) ");
                var wish = Console.ReadLine();
                if (wish == "1" || wish == "yes")
                    continue;
                else
                {
                    break;
                }
            }

            if (!isLogIn)
            {
                break;
            }

            //for Updating operation in Users or Course Table
            while (isLogIn)
            {

                Console.WriteLine("Type 1 for Teacher Entry update .");
                Console.WriteLine("Type 2 for Students Entry update .");
                Console.WriteLine("Type 3 for Course Entry update .");
                Console.WriteLine("Type 4 for log out .");
                Console.WriteLine("Type 5 for Skip and Next step .");
                num = Console.ReadLine();

                if (num == "4")
                {
                    isLogIn = false;
                    break;
                }
                else if (num == "5")
                {
                    break;
                }


                if (num != "1" && num != "2" && num != "3")
                {
                    Console.WriteLine("Please Enter 1 or 2 or 3");
                    continue;
                }

                Console.WriteLine("Enter number of Entry You want to update :: ");
                count = Convert.ToInt32(Console.ReadLine());

                //for user updating teacher or student
                if (num == "1" || num == "2")
                {
                    
                    for (int i = 0; i < count; i++)
                    {

                        Console.WriteLine($"Entry No {i + 1} out of {count}");
                        Console.WriteLine("Type username of user you want to update his/her info");

                        string tempUserName = Console.ReadLine();
                        Users targetedUser = context.Users.Where(x => x.UserName == tempUserName).FirstOrDefault();
                        if(targetedUser == null)
                        {
                            Console.WriteLine("Incorrect username ");
                            continue;
                        }
                        else
                        {
                            Console.WriteLine("Enter Full Name you want to update");
                            var fullName = Console.ReadLine();
                            targetedUser.Name = fullName;
                            //context.SaveChanges();
                            try
                            {
                                context.SaveChanges();
                            }
                            catch(Exception e)
                            {
                                Console.WriteLine($"Error is ->{e}");
                                context.ChangeTracker.Clear();
                            }

                        }
                    }
  
                }
                else
                {
                    //for updating course
                    
                    for (int i = 0; i < count; i++)
                    {
                        Console.WriteLine($"Entry No {i + 1} out of {count}");
                        Console.WriteLine("Enter course Id you want to update");
                        int id = Convert.ToInt32(Console.ReadLine());
                        Courses course = context.Courses.Where(x => x.CourseId == id).FirstOrDefault();
                        
                        if(course == null)
                        {
                            Console.WriteLine("You have entered unknown course id");
                            continue;
                        }
                        else
                        {
                            Console.WriteLine("Type 1 for update course Name");
                            Console.WriteLine("Type 2 for update Fees");
                            Console.WriteLine("Type 3 for both course Name and Fees");
                            int updateNum = Convert.ToInt32(Console.ReadLine());

                            string courseName;
                            int courseFee;

                            if(updateNum == 1)
                            {
                                Console.WriteLine("Enter Course Full Name");
                                courseName = Console.ReadLine();
                                course.CourseName = courseName;
                                //context.SaveChanges();
                                try
                                {
                                    context.SaveChanges();
                                }
                                catch (Exception e)
                                {
                                    Console.WriteLine($"Error is ->{e}");
                                    context.ChangeTracker.Clear();
                                }

                            }
                            else if(updateNum == 2)
                            {
                                Console.WriteLine("Enter Course Fee");
                                courseFee = Convert.ToInt32(Console.ReadLine());
                                course.Fees = courseFee;
                                //context.SaveChanges();
                                try
                                {
                                    context.SaveChanges();
                                }
                                catch (Exception e)
                                {
                                    Console.WriteLine($"Error is ->{e}");
                                    context.ChangeTracker.Clear();
                                }
                            }
                            else if(updateNum == 3)
                            {
                                Console.WriteLine("Enter Course Full Name");
                                courseName = Console.ReadLine();
                                Console.WriteLine("Enter Fees");
                                courseFee = Convert.ToInt32(Console.ReadLine());
                                course.CourseName = courseName;
                                course.Fees = courseFee;
                                //context.SaveChanges();
                                try
                                {
                                    context.SaveChanges();
                                }
                                catch (Exception e)
                                {
                                    Console.WriteLine($"Error is ->{e}");
                                    context.ChangeTracker.Clear();
                                }
                            }
                            else
                            {
                                continue;
                            }
                        }
                    }
                  
                }

                Console.WriteLine("Want to update more Student, Teacher, Course Entry Type 1 (yes) or 2 (no) ");
                var wish = Console.ReadLine();
                if (wish == "1" || wish == "yes")
                    continue;
                else
                {
                    break;
                }
            }

            //if selected logout option 
            if (!isLogIn)
            {
                break;
            }

            //for Deleting operation in Users and Course Table
            while (isLogIn)
            {

                Console.WriteLine("Type 1 for Teacher Entry delete .");
                Console.WriteLine("Type 2 for Students Entry delete .");
                Console.WriteLine("Type 3 for Course Entry delete .");
                Console.WriteLine("Type 4 for log out .");
                Console.WriteLine("Type 5 for Skip and Next step .");
                num = Console.ReadLine();

                if (num == "4")
                {
                    isLogIn = false;
                    break;
                }
                else if (num == "5")
                {
                    break;
                }


                if (num != "1" && num != "2" && num != "3")
                {
                    Console.WriteLine("Please Enter 1 or 2 or 3");
                    continue;
                }

                Console.WriteLine("Enter number of Entry You want to delete :: ");
                count = Convert.ToInt32(Console.ReadLine());

                //for user delete teacher or student
                if (num == "1" || num == "2")
                {

                    for (int i = 0; i < count; i++)
                    {

                        Console.WriteLine($"Entry No {i + 1} out of {count}");
                        Console.WriteLine("Type username of user you want to delete his/her info");

                        string tempUserName = Console.ReadLine();
                        Users targetedUser = context.Users.Where(x => x.UserName == tempUserName).FirstOrDefault();
                        if (targetedUser == null)
                        {
                            Console.WriteLine("Incorrect username ");
                            continue;
                        }
                        else
                        {
                            context.Users.Remove(targetedUser);
                            //context.SaveChanges();
                            try
                            {
                                context.SaveChanges();
                            }
                            catch (Exception e)
                            {
                                Console.WriteLine($"Error is ->{e}");
                                context.ChangeTracker.Clear();
                            }

                        }
                    }

                }
                else
                {
                    //for deleting course

                    for (int i = 0; i < count; i++)
                    {
                        Console.WriteLine($"Entry No {i + 1} out of {count}");
                        Console.WriteLine("Enter course Id you want to delete");
                        int id = Convert.ToInt32(Console.ReadLine());
                        Courses course = context.Courses.Where(x => x.CourseId == id).FirstOrDefault();

                        if (course == null)
                        {
                            Console.WriteLine("You have entered unknown course id");
                            continue;
                        }
                        else
                        {
                            context.Courses.Remove(course);
                            try
                            {
                                context.SaveChanges();
                            }
                            catch (Exception e)
                            {
                                Console.WriteLine($"Error is ->{e}");
                                context.ChangeTracker.Clear();
                            }

                        }
                    }

                }

                Console.WriteLine("Want to delete more Teacher, Student, Course Entry Type 1 (yes) or 2 (no) ");
                var wish = Console.ReadLine();
                if (wish == "1" || wish == "yes")
                    continue;
                else
                {
                    break;
                }
            }

            //if selected logout option 
            if (!isLogIn)
            {
                break;
            }

            //for updating Teachercourse and Studentcourse tables
            while (isLogIn)
            {

                Console.WriteLine("Type 1 for Teacher Course Updating .");
                Console.WriteLine("Type 2 for Students Course Updating .");
                Console.WriteLine("Type 3 for log out .");
                Console.WriteLine("Type 4 for skip the step");
                num = Console.ReadLine();

                if (num == "3")
                {
                    isLogIn = false;
                    break;
                }
                else if (num == "4")
                {
                    break;
                }

                if (num != "1" && num != "2")
                {
                    Console.WriteLine("Please Enter 1 or 2");
                    continue;
                }

                Console.WriteLine("Enter number of Entry You want to include :: ");
                count = Convert.ToInt32(Console.ReadLine());


                //for teacher course updating
                if (num == "1")
                {
                    
                    for (int i = 0; i < count; i++)
                    {
                        Console.WriteLine($"Entry No {i + 1} out of {count}");
                        Console.WriteLine("Enter 1 for teachers course table course teacher updating");
                        Console.WriteLine("Enter 2 for teachers course table teacher course updating");
                        
                        var updateNum = Console.ReadLine();
                        if (updateNum == "1")
                        {
                            Console.WriteLine("Enter course Id whose teacher will be update");
                            int id = Convert.ToInt32(Console.ReadLine());
                            TeachersCourse teacherCourseToUpdate = context.TeachersCourses.Where(x => x.CourseId == id).FirstOrDefault();
                            if (teacherCourseToUpdate == null)
                            {
                                Console.WriteLine("Enter valid course id");

                            }
                            else
                            {
                                Console.WriteLine("Enter new teacher username ");
                                string teacherUserName = Console.ReadLine();
                                teacherCourseToUpdate.TeacherUserName = teacherUserName;
                                //context.SaveChanges();
                                try
                                {
                                    context.SaveChanges();
                                }
                                catch (Exception e)
                                {
                                    Console.WriteLine($"Error is ->{e}");
                                    context.ChangeTracker.Clear();
                                }
                            }

                        }
                        else if (updateNum == "2")
                        {
                            Console.WriteLine("Enter teacher username ");
                            string teacherUserName = Console.ReadLine();
                            TeachersCourse teacherCourseIdToUpdate = context.TeachersCourses.Where(x => x.TeacherUserName == teacherUserName).FirstOrDefault();
                            if(teacherCourseIdToUpdate == null)
                            {
                                Console.WriteLine("Enter valid teacher user name");
                            }
                            else
                            {
                                Console.WriteLine("Enter course teacher course id which will be update");
                                int id = Convert.ToInt32(Console.ReadLine());
                                teacherCourseIdToUpdate.CourseId = id;
                                //context.SaveChanges();
                                try
                                {
                                    context.SaveChanges();
                                }
                                catch (Exception e)
                                {
                                    Console.WriteLine($"Error is ->{e}");
                                    context.ChangeTracker.Clear();
                                }
                            }

                        }
                        else
                        {
                            continue;
                        }
                    }
  
                }
                else
                {
                    //for student course update
                    for (int i = 0; i < count; i++)
                    {
                        Console.WriteLine($"Entry No {i + 1} out of {count}");
                        Console.WriteLine("Enter 1 for Student course table course student updating");
                        Console.WriteLine("Enter 2 for Student course table Student course updating");

                        var updateNum = Console.ReadLine();
                        if (updateNum == "1")
                        {
                            Console.WriteLine("Enter course Id whose student will be update");
                            int id = Convert.ToInt32(Console.ReadLine());
                            Console.WriteLine("Enter the current Student user name with courseId which will be update for the given courseId");
                            string studentName = Console.ReadLine();
                            StudentsCourses studentCourseToUpdate = context.StudentsCourses.Where(x => x.CourseId == id && x.StudentUserName == studentName).FirstOrDefault();
                            if (studentCourseToUpdate == null)
                            {
                                Console.WriteLine("Enter valid course id & student username which is already in table");

                            }
                            else
                            {
                                Console.WriteLine("Enter new student username ");
                                string newStudentUserName = Console.ReadLine();
                                studentCourseToUpdate.StudentUserName = newStudentUserName;
                                //context.SaveChanges();
                                try
                                {
                                    context.SaveChanges();
                                }
                                catch (Exception e)
                                {
                                    Console.WriteLine($"Error is ->{e}");
                                    context.ChangeTracker.Clear();
                                }
                            }

                        }
                        else if (updateNum == "2")
                        {
                            Console.WriteLine("Enter course Id which will be update");
                            int id = Convert.ToInt32(Console.ReadLine());
                            Console.WriteLine("Enter student username whose given courseId will be updated");
                            string studentName = Console.ReadLine();
                            StudentsCourses studentCourseIdToUpdate = context.StudentsCourses.Where(x => x.CourseId == id && x.StudentUserName == studentName).FirstOrDefault();
                            if (studentCourseIdToUpdate == null)
                            {
                                Console.WriteLine("Enter valid student user name & course id which is already in the table");
                            }
                            else
                            {
                                Console.WriteLine("Enter new course id which will be update");
                                int newId = Convert.ToInt32(Console.ReadLine());
                                studentCourseIdToUpdate.CourseId = newId;
                                //context.SaveChanges();
                                try
                                {
                                    context.SaveChanges();
                                }
                                catch (Exception e)
                                {
                                    Console.WriteLine($"Error is ->{e}");
                                    context.ChangeTracker.Clear();
                                }
                            }

                        }
                        else
                        {
                            continue;
                        }
                    }
                }

                Console.WriteLine("Want to update More Teacher's Course Table or Student's Course Table Entry Type 1 (yes) or 2 (no) ");
                var wish = Console.ReadLine();
                if (wish == "1" || wish == "yes")
                    continue;
                else
                {
                    break;
                }

            }
            if (!isLogIn)
            {
                break;
            }


            //for deleting TeachersCourse table & StudentsCourse table Entry
            
            while (isLogIn)
            {

                Console.WriteLine("Type 1 for Teacher Course Deleting operation .");
                Console.WriteLine("Type 2 for Students Course Deleting operation .");
                Console.WriteLine("Type 3 for log out .");
                Console.WriteLine("Type 4 for skip the step");
                num = Console.ReadLine();

                if (num == "3")
                {
                    isLogIn = false;
                    break;
                }
                else if (num == "4")
                {
                    break;
                }

                if (num != "1" && num != "2")
                {
                    Console.WriteLine("Please Enter 1 or 2");
                    continue;
                }

                Console.WriteLine("Enter number of Entry You want to include :: ");
                count = Convert.ToInt32(Console.ReadLine());


                //for teacher course deleting operation
                if (num == "1")
                {

                    for (int i = 0; i < count; i++)
                    {
                        Console.WriteLine($"Entry No {i + 1} out of {count}");
                        Console.WriteLine("Enter 1 for teachers course table teacher course deletion");

                        var updateNum = Console.ReadLine();

                        if (updateNum == "1")
                        {
                            Console.WriteLine("Enter teacher username ");
                            string teacherUserName = Console.ReadLine();
                            TeachersCourse teacherCourseToDelete = context.TeachersCourses.Where(x => x.TeacherUserName == teacherUserName).FirstOrDefault();
                            if (teacherCourseToDelete == null)
                            {
                                Console.WriteLine("Enter valid teacher user name");
                            }
                            else
                            {

                                context.TeachersCourses.Remove(teacherCourseToDelete);
                                //context.SaveChanges();
                                try
                                {
                                    context.SaveChanges();
                                }
                                catch (Exception e)
                                {
                                    Console.WriteLine($"Error is ->{e}");
                                    context.ChangeTracker.Clear();
                                }
                            }

                        }
                        else
                        {
                            continue;
                        }
                    }

                }
                else
                {
                    //for student course deletion
                    for (int i = 0; i < count; i++)
                    {
                        Console.WriteLine($"Entry No {i + 1} out of {count}");
                        Console.WriteLine("Enter 1 for Student course table course student deleting");
                     

                        var updateNum = Console.ReadLine();
                        if (updateNum == "1")
                        {
                            Console.WriteLine("Enter course Id whose student will be deleted");
                            int id = Convert.ToInt32(Console.ReadLine());
                            Console.WriteLine("Enter Student user name which will be deleted for the given courseId");
                            string studentName = Console.ReadLine();
                            StudentsCourses studentCourseToDelete = context.StudentsCourses.Where(x => x.CourseId == id && x.StudentUserName == studentName).FirstOrDefault();
                            if (studentCourseToDelete == null)
                            {
                                Console.WriteLine("Enter valid course id & student username which is already in table");

                            }
                            else
                            {
                                context.StudentsCourses.Remove(studentCourseToDelete);
                                //context.SaveChanges();
                                try
                                {
                                    context.SaveChanges();
                                }
                                catch (Exception e)
                                {
                                    Console.WriteLine($"Error is ->{e}");
                                    context.ChangeTracker.Clear();
                                }
                            }

                        }
                        else
                        {
                            continue;
                        }
                    }
                }

                Console.WriteLine("Want to delete More Teacher's Course Table or Student's Course Table Entry Type 1 (yes) or 2 (no) ");
                var wish = Console.ReadLine();
                if (wish == "1" || wish == "yes")
                    continue;
                else
                {
                    break;
                }

            }
            if (!isLogIn)
            {
                break;
            }

        }

        //when user is student
        while (type == "student" && isLogIn)
        {
            Console.WriteLine($" Hey {loggedInUser} , Your are Logged In as {type} ");
            string studentUserName = loggedInUser;
            bool isFound = true;
            bool hasClass = false;
            string num;
            int count;

            var courseList = context.StudentsCourses.Where(x => x.StudentUserName == studentUserName);

            //for giving attendence for Enrolled course class
            while (isLogIn)
            {
                Console.WriteLine("Type 1 for Giving Attendence .");
                Console.WriteLine("Type 2 for log out .");
                Console.WriteLine("Type 3 for Skip the step .");
                num = Console.ReadLine();
                //Console.WriteLine($"Input value {num}");

                if (num == "2")
                {
                    isLogIn = false;
                    break;
                }
                else if (courseList.Count() == 0)
                {
                    Console.WriteLine($"This {studentUserName} has not enrolled any course yet ");
                    isFound = false;
                    break;
                }
                else if(num == "3")
                {
                    break;
                }

                //find and print enrolled course
                List<int> courseIds = new();
                foreach (var courses in courseList)
                {
                    Console.WriteLine($"(Course Id) -> {courses.CourseId} (Logged In student) :: {courses.StudentUserName}");
                    courseIds.Add(courses.CourseId);
                }

                //find and print course class
                List<int> scheduleIds = new();
                for(int j = 0; j < courseIds.Count; j++)
                {
                    var classList = context.ClassSchedules.Where(x => x.CourseId == courseIds[j]);
                    if (classList.Count() > 0)
                        hasClass = true;
                    foreach(var classes in classList)
                    {
                        scheduleIds.Add(classes.Id);
                        Console.WriteLine($"Schedule Id -> {classes.Id} {classes.CourseId} {classes.Date} {classes.Day} {classes.StartTime} {classes.EndTime}");
                    }
                }
                if (!hasClass)
                {
                    Console.WriteLine("You dont have any class schedule availale for the enrolled courses");
                    break;
                }
                Console.WriteLine("Enter number of Attendence Entry You want to include :: ");
                count = Convert.ToInt32(Console.ReadLine());

                for (int i = 0; i < count; i++)
                {
                    Console.WriteLine($"Entry No {i + 1} out of {count}");
                    Console.WriteLine("Enter the class schedule id you want to give attendence");
                    int scheduleId = Convert.ToInt32(Console.ReadLine());

                    bool haveGot = false;
                    //Checking criteria
                    foreach(var mySchedule in scheduleIds)
                    {
                        if (mySchedule == scheduleId)
                            haveGot = true;
                    } 
                    if(!haveGot)
                    {
                        Console.WriteLine("Enter class schedule id correctly");
                        continue;
                    }
                    ClassSchedules schedule = context.ClassSchedules.Where(x => x.Id == scheduleId).FirstOrDefault();
                    var classDate = schedule.Date.ToString().Split(" ")[0];
                    var today = DateTime.Now.ToString().Split(" ")[0];
                    Console.WriteLine($" {classDate} {today}");
                    if(classDate != today)
                    {
                        Console.WriteLine("you cant give attendence ! Date is not matched with today's");
                        continue;
                    }
                    var timeToday = DateTime.Now.ToString("hh:mm tt").Split(" ");
                    double timeT = Convert.ToDouble(timeToday[0].Replace(":", "."));

                    var time = schedule.StartTime.Split(" ");
                    double time2 = Convert.ToDouble(time[0].Replace(":", "."));

                    var time3 = schedule.EndTime.Split(" ");
                    double time4 = Convert.ToDouble(time3[0].Replace(":", "."));

                    if (timeToday[1] == "PM")
                    {
                        var temp = timeT - 12.00;
                        timeT = temp + 12;
                    }

                    if (time[1] == "PM")
                    {
                        var temp = time2 - 12.00;
                        time2 = temp + 12;
                    }

                    if (time3[1] == "PM")
                    {
                        var temp = time4 - 12.00;
                        time4 = temp + 12;
                    }
                    Console.WriteLine($"{timeT} {time2} {time4}");
                    
                    if((timeT >= time2 && timeT <= time4))
                    {

                        //context.StudentsAttendences.AddRange(new StudentsAttendences { ScheduleId = scheduleId, StudentUserName = studentUserName });
                        //context.SaveChanges();
                        try
                        {
                            context.StudentsAttendences.AddRange(new StudentsAttendences { ScheduleId = scheduleId, StudentUserName = studentUserName });
                            context.SaveChanges();
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine($"Error is ->{e}");
                            context.ChangeTracker.Clear();
                        }
                    }
                    else 
                    {
                        Console.WriteLine("Your time doesnot match with class time !");
                        
                    }

                }

                Console.WriteLine("Want to Continue Attendence Entry Type 1 (yes) or 2 (no) ");
                var wish = Console.ReadLine();
                if (wish == "1" || wish == "yes")
                    continue;
                else
                    break;
            }
            if (!isLogIn || !isFound || !hasClass)
                break;
        }

        //when user is teacher
        while (type == "teacher" && isLogIn)
        {
            Console.WriteLine($" Hey {loggedInUser} , Your are Logged In as {type} ");
            string teacherUserName = loggedInUser;
            bool isFound = true;
            bool hasClass = false;
            string num;
            int count;


            //for giving attendence for Enrolled course class
            while (isLogIn)
            {
                Console.WriteLine("Type 1/anything to View Attendence Report of a student . Or ");
                Console.WriteLine("Type 2 for log out .");
                Console.WriteLine("Type 3 for Skip the step .");
                num = Console.ReadLine();
                //Console.WriteLine($"Input value {num}");

                if (num == "2")
                {
                    isLogIn = false;
                    break;
                }
                else if(num == "3")
                {
                    break;
                }

                Console.WriteLine("Enter student user name Whose attendence report you want to see .");
                string studentUserName = Console.ReadLine();
                var courseList = context.StudentsCourses.Where(x => x.StudentUserName == studentUserName);

                if (courseList.Count() == 0)
                {
                    Console.WriteLine($"This {studentUserName} has not enrolled any course yet ");
                    isFound = false;
                    break;
                }
                

                //find enrolled course
                List<int> courseIds = new();
                foreach (var courses in courseList)
                {
                    //Console.WriteLine($"(Course Id) -> {courses.CourseId} (Logged In student) :: {courses.StudentUserName}");
                    courseIds.Add(courses.CourseId);
                }

                //find and print course class report
                List<int> attendScheduleIds = new();
                var attendenceList = context.StudentsAttendences.Where(x => x.StudentUserName == studentUserName);

                for (int j = 0; j < courseIds.Count; j++)
                {
                    var classList = context.ClassSchedules.Where(x => x.CourseId == courseIds[j]);
                    if (classList.Count() > 0)
                        hasClass = true;
                    bool isGot = false;
                    foreach (var classes in classList)
                    {
                        
                        foreach(var attendence in attendenceList)
                        {
                            if (attendence.ScheduleId == classes.Id)
                            {
                                isGot = true;
                                attendScheduleIds.Add(attendence.ScheduleId);
                                break;
                            }
                                
                        }
                        if (isGot)
                        {
                            Console.WriteLine($"User Name {studentUserName}::  Schedule Id -> {classes.Id} Course -{classes.CourseId} Date -{classes.Date.ToString("dd/MM/yy")} WeekDay -{classes.Day} Start Time -{classes.StartTime} End Time -{classes.EndTime}  Total Class -{classes.TotalClass}  IsPresent => (P)");
                        }
                        else {
                            Console.WriteLine($"User Name {studentUserName}::  Schedule Id -> {classes.Id} Course -{classes.CourseId} Date -{classes.Date.ToString("dd/MM/yy")} WeekDay -{classes.Day} Start Time -{classes.StartTime} End Time -{classes.EndTime}   Total Class -{classes.TotalClass}  IsPresent => (X)");
                        }
                        isGot = false;
                        
                    }
                }
                if (!hasClass)
                {
                    Console.WriteLine("You dont have any class schedule availale for the enrolled courses");
                    break;
                }


                Console.WriteLine("Want to view Student Report Again Type 1 (yes) or 2 (no) ");
                var wish = Console.ReadLine();
                if (wish == "1" || wish == "yes")
                    continue;
                else
                    break;
            }
            if (!isLogIn || !isFound || !hasClass)
                break;
        }

    }
}
//context.Courses.Add(new Courses { CourseName = "Test", Fees = 30000});
//context.SaveChanges();
//context.Users.AddRange(
//    new Users { UserName = "student1", Name = "Ashfaq hasan", UserPassword = "12345", Type = "student" },
//    new Users { UserName = "student2", Name = "Ashfaq rahman", UserPassword = "12345", Type = "student" },
//    new Users { UserName = "student3", Name = "Ashfaq karim", UserPassword = "12345", Type = "student" },
//    new Users { UserName = "teacher1", Name = "Ashfaq aman", UserPassword = "12345", Type = "teacher" },
//    new Users { UserName = "teacher2", Name = "Ashfaq ullah", UserPassword = "12345", Type = "teacher" });

//context.Courses.AddRange(
//    new Courses { CourseName = "Bengali", Fees = 10000 },
//    new Courses { CourseName = "English1", Fees = 15000 },
//    new Courses { CourseName = "English2", Fees = 15000 },
//    new Courses { CourseName = "Math", Fees = 20000 }
//    );
//context.TeachersCourses.AddRange(
//    new TeachersCourse { CourseId=3, TeacherUserName = "teacher2"}
//    );
//context.StudentsCourses.AddRange(
//    new StudentsCourses { CourseId=2,StudentUserName="student3"},
//    new StudentsCourses { CourseId = 1, StudentUserName = "student3" },
//    new StudentsCourses { CourseId = 3, StudentUserName = "student3" },
//    new StudentsCourses { CourseId = 2, StudentUserName = "student2" },
//    new StudentsCourses { CourseId = 1, StudentUserName = "student2" },
//    new StudentsCourses { CourseId = 3, StudentUserName = "student2" }
//    );

//context.ClassSchedules.AddRange(
//    new ClassSchedules { CourseId = 1, Date = DateTime.Now, Day = DateTime.Now.DayOfWeek.ToString(), StartTime = "1PM", EndTime ="3PM",TotalClass=10 },
//    new ClassSchedules { CourseId = 1, Date = DateTime.Now, Day = DateTime.Now.DayOfWeek.ToString(), StartTime = "2PM", EndTime = "3PM", TotalClass = 10 },
//    new ClassSchedules { CourseId = 2, Date = DateTime.Now, Day = DateTime.Now.DayOfWeek.ToString(), StartTime = "4PM", EndTime = "5PM", TotalClass = 10 }
//    );

//context.Users.Remove(new Users { UserName = "student3" });
//context.SaveChanges();




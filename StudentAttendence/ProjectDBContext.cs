using Microsoft.EntityFrameworkCore;
using StudentAttendence.Seeds;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentAttendence
{
    public class ProjectDBContext : DbContext
    {
        private string _connectionString;


        public ProjectDBContext() {
            _connectionString = "Server = DESKTOP-VOUK1U4\\SQLEXPRESS; Database = StudentAttendence; User Id = ahasan; Password = 12345; TrustServerCertificate=True; MultipleActiveResultSets=true";
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {

            if (!optionsBuilder.IsConfigured)
                optionsBuilder.UseSqlServer(_connectionString);
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Users>()
                .HasKey(x => new { x.UserName });

            modelBuilder.Entity<Users>()
                .HasData(UsersSeeds.Users);

            modelBuilder.Entity<Courses>()
                .HasKey(x => new { x.CourseId });

            modelBuilder.Entity<TeachersCourse>()
                .HasKey(x => new { x.CourseId });
            modelBuilder.Entity<StudentsCourses>()
                .HasKey(x => new { x.CourseId, x.StudentUserName });

            modelBuilder.Entity<TeachersCourse>()
                .HasOne(x => x.Teacher)
                .WithMany(x => x.GivenTeacherCourse)
                .HasForeignKey(x => x.TeacherUserName);
            modelBuilder.Entity<TeachersCourse>()
                .HasOne(x => x.AssignedCourse)
                .WithMany(x => x.TeacherCourse)
                .HasForeignKey(x => x.CourseId);

            modelBuilder.Entity<StudentsCourses>()
                .HasOne(x => x.Students)
                .WithMany(x => x.GivenStudentsCourses)
                .HasForeignKey(x => x.StudentUserName);
            modelBuilder.Entity<StudentsCourses>()
                .HasOne(x => x.AssignedCourses)
                .WithMany(x => x.StudentsCourses)
                .HasForeignKey(x => x.CourseId);

            modelBuilder.Entity<ClassSchedules>()
                .HasKey(x => x.Id);
            modelBuilder.Entity<ClassSchedules>()
                .HasOne(x => x.Courses)
                .WithMany(x => x.CourseClasses)
                .HasForeignKey(x => x.CourseId);

            modelBuilder.Entity<StudentsAttendences>()
                .HasKey(x => new { x.ScheduleId, x.StudentUserName });
            modelBuilder.Entity<StudentsAttendences>()
                .HasOne(x => x.Classes)
                .WithMany(x => x.AttendClasses)
                .HasForeignKey(x => x.ScheduleId);
            //modelBuilder.Entity<StudentsAttendences>()
            //    .HasOne(x => x.StudentsCourses)
            //    .WithMany(x => x.CoursesList)
            //    .HasForeignKey(x => x.StudentUserName)

            //modelBuilder.Entity<Courses>()
            //    .HasOne(x => x.Teacher)
            //    .WithMany(x => x.TeacherCourses)
            //    .HasForeignKey(y => y.TeacherUserName)
            //    .OnDelete(DeleteBehavior.Cascade);

            //modelBuilder.Entity<Courses>()
            //    .HasOne(x => x.Student)
            //    .WithMany(x => x.StudentCourses)
            //    .HasForeignKey(y => y.StudentUserName)
            //    .OnDelete(DeleteBehavior.ClientCascade);

            //.HasForeignKey(x => x.UserName);

            base.OnModelCreating(modelBuilder);
        }
        public DbSet<Users> Users { get; set; }
        public DbSet<Courses> Courses { get; set; }
        public DbSet<TeachersCourse> TeachersCourses {get; set;}
        public DbSet<StudentsCourses> StudentsCourses {get; set;}
        public DbSet<ClassSchedules> ClassSchedules { get; set; }
        public DbSet<StudentsAttendences> StudentsAttendences { get; set; }
    }
}

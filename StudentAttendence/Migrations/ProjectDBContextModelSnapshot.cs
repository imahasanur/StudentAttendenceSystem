﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using StudentAttendence;

#nullable disable

namespace StudentAttendence.Migrations
{
    [DbContext(typeof(ProjectDBContext))]
    partial class ProjectDBContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("StudentAttendence.ClassSchedules", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("CourseId")
                        .HasColumnType("int");

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime2");

                    b.Property<string>("Day")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("EndTime")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("StartTime")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("TotalClass")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("CourseId");

                    b.ToTable("ClassSchedules");
                });

            modelBuilder.Entity("StudentAttendence.Courses", b =>
                {
                    b.Property<int>("CourseId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("CourseId"));

                    b.Property<string>("CourseName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Fees")
                        .HasColumnType("int");

                    b.HasKey("CourseId");

                    b.ToTable("Courses");
                });

            modelBuilder.Entity("StudentAttendence.StudentsAttendences", b =>
                {
                    b.Property<int>("ScheduleId")
                        .HasColumnType("int");

                    b.Property<string>("StudentUserName")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("ScheduleId", "StudentUserName");

                    b.ToTable("StudentsAttendences");
                });

            modelBuilder.Entity("StudentAttendence.StudentsCourses", b =>
                {
                    b.Property<int>("CourseId")
                        .HasColumnType("int");

                    b.Property<string>("StudentUserName")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("CourseId", "StudentUserName");

                    b.HasIndex("StudentUserName");

                    b.ToTable("StudentsCourses");
                });

            modelBuilder.Entity("StudentAttendence.TeachersCourse", b =>
                {
                    b.Property<int>("CourseId")
                        .HasColumnType("int");

                    b.Property<string>("TeacherUserName")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("CourseId");

                    b.HasIndex("TeacherUserName");

                    b.ToTable("TeachersCourses");
                });

            modelBuilder.Entity("StudentAttendence.Users", b =>
                {
                    b.Property<string>("UserName")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Type")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserPassword")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserName");

                    b.ToTable("Users");

                    b.HasData(
                        new
                        {
                            UserName = "admin",
                            Name = "Mr Admin",
                            Type = "admin",
                            UserPassword = "12345"
                        });
                });

            modelBuilder.Entity("StudentAttendence.ClassSchedules", b =>
                {
                    b.HasOne("StudentAttendence.Courses", "Courses")
                        .WithMany("CourseClasses")
                        .HasForeignKey("CourseId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Courses");
                });

            modelBuilder.Entity("StudentAttendence.StudentsAttendences", b =>
                {
                    b.HasOne("StudentAttendence.ClassSchedules", "Classes")
                        .WithMany("AttendClasses")
                        .HasForeignKey("ScheduleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Classes");
                });

            modelBuilder.Entity("StudentAttendence.StudentsCourses", b =>
                {
                    b.HasOne("StudentAttendence.Courses", "AssignedCourses")
                        .WithMany("StudentsCourses")
                        .HasForeignKey("CourseId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("StudentAttendence.Users", "Students")
                        .WithMany("GivenStudentsCourses")
                        .HasForeignKey("StudentUserName")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("AssignedCourses");

                    b.Navigation("Students");
                });

            modelBuilder.Entity("StudentAttendence.TeachersCourse", b =>
                {
                    b.HasOne("StudentAttendence.Courses", "AssignedCourse")
                        .WithMany("TeacherCourse")
                        .HasForeignKey("CourseId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("StudentAttendence.Users", "Teacher")
                        .WithMany("GivenTeacherCourse")
                        .HasForeignKey("TeacherUserName")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("AssignedCourse");

                    b.Navigation("Teacher");
                });

            modelBuilder.Entity("StudentAttendence.ClassSchedules", b =>
                {
                    b.Navigation("AttendClasses");
                });

            modelBuilder.Entity("StudentAttendence.Courses", b =>
                {
                    b.Navigation("CourseClasses");

                    b.Navigation("StudentsCourses");

                    b.Navigation("TeacherCourse");
                });

            modelBuilder.Entity("StudentAttendence.Users", b =>
                {
                    b.Navigation("GivenStudentsCourses");

                    b.Navigation("GivenTeacherCourse");
                });
#pragma warning restore 612, 618
        }
    }
}

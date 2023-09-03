using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StudentAttendence.Migrations
{
    /// <inheritdoc />
    public partial class MakingUsersCoursesTeacherCoursesStudentsCoursesTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Courses",
                columns: table => new
                {
                    CourseId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CourseName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Fees = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Courses", x => x.CourseId);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    UserName = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserPassword = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Type = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.UserName);
                });

            migrationBuilder.CreateTable(
                name: "StudentsCourses",
                columns: table => new
                {
                    CourseId = table.Column<int>(type: "int", nullable: false),
                    StudentUserName = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StudentsCourses", x => new { x.CourseId, x.StudentUserName });
                    table.ForeignKey(
                        name: "FK_StudentsCourses_Courses_CourseId",
                        column: x => x.CourseId,
                        principalTable: "Courses",
                        principalColumn: "CourseId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_StudentsCourses_Users_StudentUserName",
                        column: x => x.StudentUserName,
                        principalTable: "Users",
                        principalColumn: "UserName",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TeachersCourses",
                columns: table => new
                {
                    CourseId = table.Column<int>(type: "int", nullable: false),
                    TeacherUserName = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TeachersCourses", x => x.CourseId);
                    table.ForeignKey(
                        name: "FK_TeachersCourses_Courses_CourseId",
                        column: x => x.CourseId,
                        principalTable: "Courses",
                        principalColumn: "CourseId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TeachersCourses_Users_TeacherUserName",
                        column: x => x.TeacherUserName,
                        principalTable: "Users",
                        principalColumn: "UserName",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "UserName", "Name", "Type", "UserPassword" },
                values: new object[] { "admin", "Mr Admin", "admin", "12345" });

            migrationBuilder.CreateIndex(
                name: "IX_StudentsCourses_StudentUserName",
                table: "StudentsCourses",
                column: "StudentUserName");

            migrationBuilder.CreateIndex(
                name: "IX_TeachersCourses_TeacherUserName",
                table: "TeachersCourses",
                column: "TeacherUserName");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "StudentsCourses");

            migrationBuilder.DropTable(
                name: "TeachersCourses");

            migrationBuilder.DropTable(
                name: "Courses");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}

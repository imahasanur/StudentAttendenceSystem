using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StudentAttendence.Migrations
{
    /// <inheritdoc />
    public partial class MakingAttendenceTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "StudentsAttendences",
                columns: table => new
                {
                    ScheduleId = table.Column<int>(type: "int", nullable: false),
                    StudentUserName = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StudentsAttendences", x => new { x.ScheduleId, x.StudentUserName });
                    table.ForeignKey(
                        name: "FK_StudentsAttendences_ClassSchedules_ScheduleId",
                        column: x => x.ScheduleId,
                        principalTable: "ClassSchedules",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "StudentsAttendences");
        }
    }
}

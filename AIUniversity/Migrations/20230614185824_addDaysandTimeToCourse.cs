using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AIUniversity.Migrations
{
    public partial class addDaysandTimeToCourse : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "DaysOfTheWeek",
                table: "Courses",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "TimeOfClass",
                table: "Courses",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DaysOfTheWeek",
                table: "Courses");

            migrationBuilder.DropColumn(
                name: "TimeOfClass",
                table: "Courses");
        }
    }
}

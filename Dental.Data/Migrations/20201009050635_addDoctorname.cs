using Microsoft.EntityFrameworkCore.Migrations;

namespace Dental.Data.Migrations
{
    public partial class addDoctorname : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "DoctorName",
                table: "Schedules",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DoctorName",
                table: "Messages",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DoctorName",
                table: "Bills",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DoctorName",
                table: "Appointments",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DoctorName",
                table: "Schedules");

            migrationBuilder.DropColumn(
                name: "DoctorName",
                table: "Messages");

            migrationBuilder.DropColumn(
                name: "DoctorName",
                table: "Bills");

            migrationBuilder.DropColumn(
                name: "DoctorName",
                table: "Appointments");
        }
    }
}

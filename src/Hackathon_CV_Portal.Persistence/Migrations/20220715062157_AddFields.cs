using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Hackathon_CV_Portal.Persistence.Migrations
{
    public partial class AddFields : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Currency",
                table: "Vacancies");

            migrationBuilder.RenameColumn(
                name: "Salary",
                table: "Vacancies",
                newName: "Type");

            migrationBuilder.AddColumn<string>(
                name: "CompanyName",
                table: "Vacancies",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Location",
                table: "Vacancies",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "SalaryRange",
                table: "Vacancies",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CompanyName",
                table: "Vacancies");

            migrationBuilder.DropColumn(
                name: "Location",
                table: "Vacancies");

            migrationBuilder.DropColumn(
                name: "SalaryRange",
                table: "Vacancies");

            migrationBuilder.RenameColumn(
                name: "Type",
                table: "Vacancies",
                newName: "Salary");

            migrationBuilder.AddColumn<string>(
                name: "Currency",
                table: "Vacancies",
                type: "nvarchar(15)",
                maxLength: 15,
                nullable: false,
                defaultValue: "");
        }
    }
}

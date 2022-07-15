using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Hackathon_CV_Portal.Persistence.Migrations
{
    public partial class NameChanges : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AppliedDate",
                table: "FavouriteVacancies");

            migrationBuilder.RenameColumn(
                name: "VacancyId",
                table: "FavouriteVacancies",
                newName: "VacansyId");

            migrationBuilder.RenameColumn(
                name: "CurriculumVitaeId",
                table: "FavouriteVacancies",
                newName: "UserId");

            migrationBuilder.RenameColumn(
                name: "VacansyId",
                table: "AppliedCurriculumVitaes",
                newName: "VacancyId");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "AppliedCurriculumVitaes",
                newName: "CurriculumVitaeId");

            migrationBuilder.AddColumn<DateTime>(
                name: "AppliedDate",
                table: "AppliedCurriculumVitaes",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AppliedDate",
                table: "AppliedCurriculumVitaes");

            migrationBuilder.RenameColumn(
                name: "VacansyId",
                table: "FavouriteVacancies",
                newName: "VacancyId");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "FavouriteVacancies",
                newName: "CurriculumVitaeId");

            migrationBuilder.RenameColumn(
                name: "VacancyId",
                table: "AppliedCurriculumVitaes",
                newName: "VacansyId");

            migrationBuilder.RenameColumn(
                name: "CurriculumVitaeId",
                table: "AppliedCurriculumVitaes",
                newName: "UserId");

            migrationBuilder.AddColumn<DateTime>(
                name: "AppliedDate",
                table: "FavouriteVacancies",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }
    }
}

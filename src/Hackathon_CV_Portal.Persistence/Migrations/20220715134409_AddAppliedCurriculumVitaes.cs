using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Hackathon_CV_Portal.Persistence.Migrations
{
    public partial class AddAppliedCurriculumVitaes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "VacansyId",
                table: "FavouriteVacancies",
                newName: "VacancyId");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "FavouriteVacancies",
                newName: "CurriculumVitaeId");

            migrationBuilder.CreateTable(
                name: "AppliedCurriculumVitaes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    VacansyId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppliedCurriculumVitaes", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AppliedCurriculumVitaes");

            migrationBuilder.RenameColumn(
                name: "VacancyId",
                table: "FavouriteVacancies",
                newName: "VacansyId");

            migrationBuilder.RenameColumn(
                name: "CurriculumVitaeId",
                table: "FavouriteVacancies",
                newName: "UserId");
        }
    }
}

using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Hackathon_CV_Portal.Persistence.Migrations
{
    public partial class AddVacancyQualiAndRespo : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Qualifications",
                table: "Vacancies");

            migrationBuilder.DropColumn(
                name: "Responsibility",
                table: "Vacancies");

            migrationBuilder.CreateTable(
                name: "Qualifications",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    QualificationName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    VacancyId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Qualifications", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Qualifications_Vacancies_VacancyId",
                        column: x => x.VacancyId,
                        principalTable: "Vacancies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Responsibilities",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ResponsibilityName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    VacancyId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Responsibilities", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Responsibilities_Vacancies_VacancyId",
                        column: x => x.VacancyId,
                        principalTable: "Vacancies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Qualifications_VacancyId",
                table: "Qualifications",
                column: "VacancyId");

            migrationBuilder.CreateIndex(
                name: "IX_Responsibilities_VacancyId",
                table: "Responsibilities",
                column: "VacancyId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Qualifications");

            migrationBuilder.DropTable(
                name: "Responsibilities");

            migrationBuilder.AddColumn<string>(
                name: "Qualifications",
                table: "Vacancies",
                type: "NVARCHAR(500)",
                maxLength: 500,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Responsibility",
                table: "Vacancies",
                type: "NVARCHAR(500)",
                maxLength: 500,
                nullable: false,
                defaultValue: "");
        }
    }
}

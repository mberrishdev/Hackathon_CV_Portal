using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Hackathon_CV_Portal.Persistence.Migrations
{
    public partial class AddFieldToAppliedCVs : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "AppliedDate",
                table: "FavouriteVacancies",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AppliedDate",
                table: "FavouriteVacancies");
        }
    }
}

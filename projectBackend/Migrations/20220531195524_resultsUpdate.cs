using Microsoft.EntityFrameworkCore.Migrations;

namespace projectBackend.Migrations
{
    public partial class resultsUpdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "QuestionaireID",
                table: "Results",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "QuestionaireID",
                table: "Results");
        }
    }
}

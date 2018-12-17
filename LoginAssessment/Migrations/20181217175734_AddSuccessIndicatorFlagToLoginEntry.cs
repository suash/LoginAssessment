using Microsoft.EntityFrameworkCore.Migrations;

namespace LoginAssessment.Migrations
{
    public partial class AddSuccessIndicatorFlagToLoginEntry : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsSuccessful",
                table: "LoginEntry",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsSuccessful",
                table: "LoginEntry");
        }
    }
}

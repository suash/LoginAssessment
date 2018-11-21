using Microsoft.EntityFrameworkCore.Migrations;

namespace LoginAssessment.Migrations
{
    public partial class NewChanges : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Comments",
                table: "LoginSuccess",
                maxLength: 5000,
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsPreviousPasswordOrPassphraseUsed",
                table: "LoginSuccess",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Comments",
                table: "LoginFail",
                maxLength: 5000,
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsPreviousPasswordOrPassphraseUsed",
                table: "LoginFail",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Password",
                table: "AspNetUsers",
                maxLength: 255,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Comments",
                table: "LoginSuccess");

            migrationBuilder.DropColumn(
                name: "IsPreviousPasswordOrPassphraseUsed",
                table: "LoginSuccess");

            migrationBuilder.DropColumn(
                name: "Comments",
                table: "LoginFail");

            migrationBuilder.DropColumn(
                name: "IsPreviousPasswordOrPassphraseUsed",
                table: "LoginFail");

            migrationBuilder.DropColumn(
                name: "Password",
                table: "AspNetUsers");
        }
    }
}

using Microsoft.EntityFrameworkCore.Migrations;

namespace LoginAssessment.Migrations
{
    public partial class UpdatePasswordUsedBeforeFields : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsPreviousPasswordOrPassphraseUsed",
                table: "LoginSuccess");

            migrationBuilder.DropColumn(
                name: "IsPreviousPasswordOrPassphraseUsed",
                table: "LoginFail");

            migrationBuilder.AddColumn<bool>(
                name: "IsPreviousPassphraseUsed",
                table: "LoginSuccess",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsPreviousPasswordAndPassphraseUsed",
                table: "LoginSuccess",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsPreviousPasswordUsed",
                table: "LoginSuccess",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsPreviousPassphraseUsed",
                table: "LoginFail",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsPreviousPasswordAndPassphraseUsed",
                table: "LoginFail",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsPreviousPasswordUsed",
                table: "LoginFail",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsPreviousPassphraseUsed",
                table: "LoginSuccess");

            migrationBuilder.DropColumn(
                name: "IsPreviousPasswordAndPassphraseUsed",
                table: "LoginSuccess");

            migrationBuilder.DropColumn(
                name: "IsPreviousPasswordUsed",
                table: "LoginSuccess");

            migrationBuilder.DropColumn(
                name: "IsPreviousPassphraseUsed",
                table: "LoginFail");

            migrationBuilder.DropColumn(
                name: "IsPreviousPasswordAndPassphraseUsed",
                table: "LoginFail");

            migrationBuilder.DropColumn(
                name: "IsPreviousPasswordUsed",
                table: "LoginFail");

            migrationBuilder.AddColumn<bool>(
                name: "IsPreviousPasswordOrPassphraseUsed",
                table: "LoginSuccess",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsPreviousPasswordOrPassphraseUsed",
                table: "LoginFail",
                nullable: true);
        }
    }
}

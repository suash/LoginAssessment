using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace LoginAssessment.Migrations
{
    public partial class AddLoginHistoryAndLogging : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "LoginTypeId",
                table: "LoginSuccess",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "LoginTypeId",
                table: "LoginFail",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "LoginType",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LoginType", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PassphraseChange",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    UserId = table.Column<string>(nullable: true),
                    PassphraseBefore = table.Column<string>(nullable: true),
                    PassphraseAfter = table.Column<string>(nullable: true),
                    PassphraseChangedDateTime = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PassphraseChange", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PassphraseChange_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PasswordChange",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    UserId = table.Column<string>(nullable: true),
                    PasswordBefore = table.Column<string>(nullable: true),
                    PasswordAfter = table.Column<string>(nullable: true),
                    PasswordChangedDateTime = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PasswordChange", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PasswordChange_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "LoginEntry",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    UserId = table.Column<string>(nullable: true),
                    LoginTypeId = table.Column<int>(nullable: false),
                    LoginDateTime = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LoginEntry", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LoginEntry_LoginType_LoginTypeId",
                        column: x => x.LoginTypeId,
                        principalTable: "LoginType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_LoginEntry_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_LoginSuccess_LoginTypeId",
                table: "LoginSuccess",
                column: "LoginTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_LoginFail_LoginTypeId",
                table: "LoginFail",
                column: "LoginTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_LoginEntry_LoginTypeId",
                table: "LoginEntry",
                column: "LoginTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_LoginEntry_UserId",
                table: "LoginEntry",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_PassphraseChange_UserId",
                table: "PassphraseChange",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_PasswordChange_UserId",
                table: "PasswordChange",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_LoginFail_LoginType_LoginTypeId",
                table: "LoginFail",
                column: "LoginTypeId",
                principalTable: "LoginType",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_LoginSuccess_LoginType_LoginTypeId",
                table: "LoginSuccess",
                column: "LoginTypeId",
                principalTable: "LoginType",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LoginFail_LoginType_LoginTypeId",
                table: "LoginFail");

            migrationBuilder.DropForeignKey(
                name: "FK_LoginSuccess_LoginType_LoginTypeId",
                table: "LoginSuccess");

            migrationBuilder.DropTable(
                name: "LoginEntry");

            migrationBuilder.DropTable(
                name: "PassphraseChange");

            migrationBuilder.DropTable(
                name: "PasswordChange");

            migrationBuilder.DropTable(
                name: "LoginType");

            migrationBuilder.DropIndex(
                name: "IX_LoginSuccess_LoginTypeId",
                table: "LoginSuccess");

            migrationBuilder.DropIndex(
                name: "IX_LoginFail_LoginTypeId",
                table: "LoginFail");

            migrationBuilder.DropColumn(
                name: "LoginTypeId",
                table: "LoginSuccess");

            migrationBuilder.DropColumn(
                name: "LoginTypeId",
                table: "LoginFail");
        }
    }
}

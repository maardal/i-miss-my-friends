using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace immfApi.Migrations
{
    /// <inheritdoc />
    public partial class HangoutAndLovedoneCreation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "LovedOnes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    Relationship = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LovedOnes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Hangouts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Date = table.Column<DateTime>(type: "TEXT", nullable: false),
                    LovedOneId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Hangouts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Hangouts_LovedOnes_LovedOneId",
                        column: x => x.LovedOneId,
                        principalTable: "LovedOnes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Hangouts_LovedOneId",
                table: "Hangouts",
                column: "LovedOneId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Hangouts");

            migrationBuilder.DropTable(
                name: "LovedOnes");
        }
    }
}

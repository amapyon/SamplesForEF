using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace KptBoardSystem.Migrations
{
    public partial class step02 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "KptBards",
                columns: table => new
                {
                    KptBoardId = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Keep = table.Column<string>(nullable: true),
                    Problem = table.Column<string>(nullable: true),
                    Time = table.Column<DateTime>(nullable: false),
                    Try = table.Column<string>(nullable: true),
                    UserId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KptBards", x => x.KptBoardId);
                    table.ForeignKey(
                        name: "FK_KptBards_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_KptBards_UserId",
                table: "KptBards",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "KptBards");
        }
    }
}

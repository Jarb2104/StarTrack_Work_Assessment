using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SearchStatisticsDB.Migrations
{
    /// <inheritdoc />
    public partial class CreateDB : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "StackExchangeCalls",
                columns: table => new
                {
                    QueryID = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Page = table.Column<int>(type: "INTEGER", nullable: false),
                    PageSize = table.Column<int>(type: "INTEGER", nullable: false),
                    QueryText = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StackExchangeCalls", x => x.QueryID);
                });

            migrationBuilder.CreateTable(
                name: "QueryResults",
                columns: table => new
                {
                    ResultID = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Tittle = table.Column<string>(type: "TEXT", nullable: false),
                    AnswerCount = table.Column<int>(type: "INTEGER", nullable: false),
                    UserName = table.Column<string>(type: "TEXT", nullable: false),
                    PicURL = table.Column<string>(type: "TEXT", nullable: false),
                    QueryDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    StackExchangeCallQueryID = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QueryResults", x => x.ResultID);
                    table.ForeignKey(
                        name: "FK_QueryResults_StackExchangeCalls_StackExchangeCallQueryID",
                        column: x => x.StackExchangeCallQueryID,
                        principalTable: "StackExchangeCalls",
                        principalColumn: "QueryID");
                });

            migrationBuilder.CreateIndex(
                name: "IX_QueryResults_StackExchangeCallQueryID",
                table: "QueryResults",
                column: "StackExchangeCallQueryID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "QueryResults");

            migrationBuilder.DropTable(
                name: "StackExchangeCalls");
        }
    }
}

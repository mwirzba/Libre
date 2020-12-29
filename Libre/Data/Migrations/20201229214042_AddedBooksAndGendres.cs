using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Libre.Data.Migrations
{
    public partial class AddedBooksAndGendres : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Gendre",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Gendre", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Book",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Title = table.Column<string>(nullable: true),
                    Publisher = table.Column<string>(nullable: true),
                    RealeaseDate = table.Column<DateTime>(nullable: false),
                    Language = table.Column<string>(nullable: true),
                    Pages = table.Column<int>(nullable: false),
                    CoverType = table.Column<string>(nullable: true),
                    Info = table.Column<string>(nullable: true),
                    GendreId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Book", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Book_Gendre_GendreId",
                        column: x => x.GendreId,
                        principalTable: "Gendre",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Book_GendreId",
                table: "Book",
                column: "GendreId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Book");

            migrationBuilder.DropTable(
                name: "Gendre");
        }
    }
}

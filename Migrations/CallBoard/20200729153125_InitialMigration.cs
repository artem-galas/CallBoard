using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CallBoard.Migrations.CallBoard
{
    public partial class InitialMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Comments",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Text = table.Column<string>(nullable: true),
                    AuthorId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Comments", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Posts",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Title = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    ImageUrl = table.Column<string>(nullable: true),
                    Price = table.Column<double>(nullable: false),
                    AuthorId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Posts", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Posts",
                columns: new[] { "Id", "AuthorId", "Description", "ImageUrl", "Price", "Title" },
                values: new object[] { new Guid("b30603ef-564a-48ea-8eef-e2f3d2fb8ae7"), new Guid("dac783d3-be9a-4205-9df8-ad8f145a87ea"), "No one has ever stopped the Caped Crusader. Not The Joker. Not Two-Face. Not even the entire Justice League. But how does Batman confront a new hero who wants to save the city from the Dark Knight?", "https://res.cloudinary.com/dkzhgauk1/image/upload/v1579376877/Bat01_vgtkc0.jpg", 2.9900000000000002, "“I AM GOTHAM” Chapter One" });

            migrationBuilder.InsertData(
                table: "Posts",
                columns: new[] { "Id", "AuthorId", "Description", "ImageUrl", "Price", "Title" },
                values: new object[] { new Guid("60efed75-efc0-48ec-91cd-1f525ea59a06"), new Guid("dac783d3-be9a-4205-9df8-ad8f145a87ea"), "Running parallel to Greg Rucka and Liam Sharp’s “The Lies,” Rucka and artist Nicola Scott weave the definitive and shocking tale of Diana’s first year as Earth’s protector.", "https://res.cloudinary.com/dkzhgauk1/image/upload/v1579376883/ww02_j7mzd8.jpg", 1.99, "WONDER WOMAN YEAR ONE #1" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Comments");

            migrationBuilder.DropTable(
                name: "Posts");
        }
    }
}

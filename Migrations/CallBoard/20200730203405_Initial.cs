using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CallBoard.Migrations.CallBoard
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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

            migrationBuilder.CreateTable(
                name: "Comments",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Text = table.Column<string>(nullable: true),
                    AuthorId = table.Column<Guid>(nullable: false),
                    PostId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Comments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Comments_Posts_PostId",
                        column: x => x.PostId,
                        principalTable: "Posts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Posts",
                columns: new[] { "Id", "AuthorId", "Description", "ImageUrl", "Price", "Title" },
                values: new object[] { new Guid("2557e760-dbab-4f14-baa5-5bbf36c4ee51"), new Guid("dac783d3-be9a-4205-9df8-ad8f145a87ea"), "No one has ever stopped the Caped Crusader. Not The Joker. Not Two-Face. Not even the entire Justice League. But how does Batman confront a new hero who wants to save the city from the Dark Knight?", "https://res.cloudinary.com/dkzhgauk1/image/upload/v1579376877/Bat01_vgtkc0.jpg", 2.9900000000000002, "“I AM GOTHAM” Chapter One" });

            migrationBuilder.InsertData(
                table: "Posts",
                columns: new[] { "Id", "AuthorId", "Description", "ImageUrl", "Price", "Title" },
                values: new object[] { new Guid("975e2531-e060-4aa4-a027-05cc5c197c88"), new Guid("dac783d3-be9a-4205-9df8-ad8f145a87ea"), "Running parallel to Greg Rucka and Liam Sharp’s “The Lies,” Rucka and artist Nicola Scott weave the definitive and shocking tale of Diana’s first year as Earth’s protector.", "https://res.cloudinary.com/dkzhgauk1/image/upload/v1579376883/ww02_j7mzd8.jpg", 1.99, "WONDER WOMAN YEAR ONE #1" });

            migrationBuilder.CreateIndex(
                name: "IX_Comments_PostId",
                table: "Comments",
                column: "PostId");
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

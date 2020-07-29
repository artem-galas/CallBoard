using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;

using CallBoard.Models;

namespace CallBoard.DbContexts
{
    public class CallBoardContext : DbContext
    {
        public CallBoardContext(DbContextOptions<CallBoardContext> options)
        : base(options)
        { }

        public DbSet<PostModel> Posts { get; set; }
        public DbSet<CommentModel> Comments { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // seed the database with dummy data
            modelBuilder.Entity<PostModel>().HasData(

                new PostModel()
                {
                    Id = Guid.NewGuid(),
                    Price = 2.99,
                    Title = "“I AM GOTHAM” Chapter One",
                    Description = "No one has ever stopped the Caped Crusader. Not The Joker. Not Two-Face. Not even the entire Justice League. But how does Batman confront a new hero who wants to save the city from the Dark Knight?",
                    ImageUrl = "https://res.cloudinary.com/dkzhgauk1/image/upload/v1579376877/Bat01_vgtkc0.jpg",
                    AuthorId = Guid.Parse("dac783d3-be9a-4205-9df8-ad8f145a87ea"),
                },
                new PostModel() {
                    Id = Guid.NewGuid(),
                    Price = 1.99,
                    Title = "WONDER WOMAN YEAR ONE #1",
                    Description = "Running parallel to Greg Rucka and Liam Sharp’s “The Lies,” Rucka and artist Nicola Scott weave the definitive and shocking tale of Diana’s first year as Earth’s protector.",
                    ImageUrl = "https://res.cloudinary.com/dkzhgauk1/image/upload/v1579376883/ww02_j7mzd8.jpg",
                    AuthorId = Guid.Parse("dac783d3-be9a-4205-9df8-ad8f145a87ea"),
                }
            );

            base.OnModelCreating(modelBuilder);
        }
    }
}
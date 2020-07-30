using System;
using System.Collections.Generic;

namespace CallBoard.Models
{
    public class Post
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string ImageUrl { get; set; }
        public double Price { get; set; }
        public Guid AuthorId { get; set; }
        public ICollection<Comment> Comments { get; set; }
    }
}
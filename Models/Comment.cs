using System;

namespace CallBoard.Models
{
    public class Comment
    {
        public Guid Id { get; set; }
        public string Text { get; set; }
        public Guid AuthorId { get; set; }
        public Guid PostId { get; set; }
    }
}
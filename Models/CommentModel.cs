using System;

namespace CallBoard.Models
{
    public class CommentModel
    {
        public Guid Id { get; set; }
        public string Text { get; set; }
        public Guid AuthorId { get; set; }
    }
}
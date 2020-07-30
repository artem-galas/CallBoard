using System;
using System.Collections.Generic;
using System.Linq;

using CallBoard.DbContexts;
using CallBoard.Models;

namespace CallBoard.Repository
{
    public class PostRepository : IPostRepository
    {
        private readonly CallBoardContext _context;

        public PostRepository(CallBoardContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public IEnumerable<PostModel> GetPosts()
        {
            return _context.Posts.ToList();
        }

        public void AddPost(Guid authorId, PostModel post)
        {
            if (authorId == Guid.Empty)
            {
                throw new ArgumentNullException(nameof(authorId));
            }

            if (post == null)
            {
                throw new ArgumentNullException(nameof(post));
            }

            post.AuthorId = authorId;
            _context.Posts.Add(post);
        }

        public PostModel GetPost(Guid postId)
        {
            if (postId == Guid.Empty)
            {
                throw new ArgumentNullException(nameof(postId));
            }

            return _context.Posts.Find(postId);
        }

        public PostModel CreatePost(PostModel post)
        {
            _context.Posts.Add(post);
            _context.SaveChanges();

            return post;
        }

        public PostModel UpdatePost(Guid postId, PostModel postData)
        {
            var post = _context.Posts.FirstOrDefault(p => p.Id == postId);
            _context.Posts.Update(postData);
            _context.SaveChanges();

            return post;
        }

    }
}
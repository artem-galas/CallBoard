using System;
using System.Collections.Generic;

using CallBoard.Models;

namespace CallBoard
{
    public interface IPostRepository
    {
        IEnumerable<PostModel> GetPosts();
        void AddPost(Guid autorId, PostModel post);
        PostModel GetPost(Guid postId);
    }
}
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using CallBoard.Models;

using CallBoard.Repository;

namespace CallBoard.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IPostRepository _postRepo;

        public HomeController(ILogger<HomeController> logger, IPostRepository postRepo)
        {
            _logger = logger;
            _postRepo = postRepo ??
                throw new ArgumentNullException(nameof(postRepo));
        }

        public IActionResult Index()
        {
            var posts = _postRepo.GetPosts();
            return View(posts);
        }

        public IActionResult Details(Guid id)
        {
            var post = _postRepo.GetPost(id);
            if (post == null)
            {
                return NotFound();
            }

            return View(post);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}

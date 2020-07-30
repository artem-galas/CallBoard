using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using CallBoard.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;

using CallBoard.Repository;

namespace CallBoard.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IPostRepository _postRepo;
        private UserManager<IdentityUser> _userManager;

        public HomeController(ILogger<HomeController> logger, IPostRepository postRepo, UserManager<IdentityUser> userManager)
        {
            _logger = logger;
            _userManager = userManager;
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

        [Authorize]
        public IActionResult Create()
        {
            return View();
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("Title,Description,Price,ImageUrl")] PostModel post)
        {
            try {
                if (ModelState.IsValid)
                {
                    var userId = _userManager.GetUserId(User);
                    post.AuthorId = Guid.Parse(userId);
                    _postRepo.CreatePost(post);
                    return RedirectToAction(nameof(Index));
                }
            } catch (DbUpdateException /* ex */)
            {
                ModelState.AddModelError("", "Unable to save changes. " +
                    "Try again, and if the problem persists " +
                    "see your system administrator.");
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

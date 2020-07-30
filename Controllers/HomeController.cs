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
using CallBoard.DbContexts;

namespace CallBoard.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IPostRepository _postRepo;
        private readonly UserManager<IdentityUser> _userManager;
         private readonly CallBoardContext _context;

        public HomeController(ILogger<HomeController> logger, 
            IPostRepository postRepo, 
            UserManager<IdentityUser> userManager,
            CallBoardContext context)
        {
            _logger = logger;
            _userManager = userManager;
            _context = context;
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

        [Authorize]
        public IActionResult Edit(Guid id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var post = _postRepo.GetPost(id);
            if (post == null)
            {
                return NotFound();
            }
            return View(post);
        }

        [Authorize]
        [HttpPost, ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditPost(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var postToUpdate = await _context.Posts
                .FirstOrDefaultAsync(p => p.Id == id);

            if (await TryUpdateModelAsync<PostModel>(postToUpdate,
                "",
                p => p.ImageUrl, p => p.Title, p => p.Description, p => p.Price))
            {
                try
                {
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateException /* ex */)
                {
                    //Log the error (uncomment ex variable name and write a log.)
                    ModelState.AddModelError("", "Unable to save changes. " +
                        "Try again, and if the problem persists, " +
                        "see your system administrator.");
                }
                return RedirectToAction(nameof(Index));
            }

            return View(postToUpdate);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}

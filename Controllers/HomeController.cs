using Microsoft.AspNetCore.Mvc;
using MyBlog.Data;
using MyBlog.Models;
using System.Diagnostics;

namespace MyBlog.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly MysqlContext _context;

        public HomeController(ILogger<HomeController> logger, MysqlContext c)
        {
            _logger = logger;
            _context = c;
        }

        public IActionResult Index()
        {
            List<Post> posts = _context.Posts.ToList();
            return View(posts);
        }

        public IActionResult Detail(int id)
        {
            Post pos = _context.Posts.FirstOrDefault(p => p.Id == id);
            return View(pos);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
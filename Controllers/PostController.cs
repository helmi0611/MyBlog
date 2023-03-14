using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyBlog.Data;
using MyBlog.Models;

namespace MyBlog.Controllers
{
    [Authorize]
    public class PostController : Controller
    {
       
        private readonly MysqlContext _context;
        public PostController(MysqlContext c)
        {

            _context = c;
        }

        public IActionResult Index()
        {
            var Pots = _context.Posts.ToList();

            return View(Pots);
        }
        public IActionResult PostCreate()
        {
            return View();
        }
        [HttpPost]
        public IActionResult PostCreate([FromForm] Post data)
        {
            _context.Posts.Add(data);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
        public IActionResult PostEdit(int id)
        {
            var user = _context.Posts.FirstOrDefault(x => x.Id == id);
            return View(user);
        }
        [HttpPost]
        public IActionResult PostEdit([FromForm] Post data)
        {

            _context.Posts.Update(data);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }

    }
}

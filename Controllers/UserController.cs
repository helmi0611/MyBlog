using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyBlog.Data;
using MyBlog.Models;
using System.Security.Claims;

namespace MyBlog.Controllers
{
    [Authorize]
    public class UserController : Controller
    {
        private readonly MysqlContext _context;
        public UserController (MysqlContext c)
        {

            _context = c;
        }

        public IActionResult Index()
        {
            var users = _context.Users.ToList();

            return View(users);
        }

        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create ([FromForm] User data)
        {
            _context.Users.Add(data);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }
        public IActionResult Edit(int id)
        {
            var user = _context.Users.FirstOrDefault(x => x.Id == id);
            return View(user);
        }
        [HttpPost]
        public IActionResult Edit([FromForm] User data)
        {

            _context.Users.Update(data);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }

        public IActionResult Delete(int id)
        {
            var user = _context.Users.FirstOrDefault(x => x.Id == id);
            _context.Users.Remove(user);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}

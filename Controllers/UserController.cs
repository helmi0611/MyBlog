﻿using Microsoft.AspNetCore.Authorization;
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
        private readonly IWebHostEnvironment _env;
        public UserController (MysqlContext c, IWebHostEnvironment env)
        {

            _context = c;
            _env = env;
        }

        public IActionResult Index()
        {
            var users = _context.Users.ToList();

            return View(users);
        }
        public IActionResult Detail(int id)
        {
            var userst = _context.Users.FirstOrDefault(x => x.Id == id);

            return View(userst);
        }
        public IActionResult Download(int id)
        {
            var user = _context.Users.FirstOrDefault(x => x.Id == id);
            var filepath = Path.Combine(_env.WebRootPath, "upload", user.Photo);

            return File(
                System.IO.File.ReadAllBytes(filepath), "image/png",
                Path.GetFileName(filepath));
        }


        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create ([FromForm] User data, IFormFile Photo)
        {
            if(Photo.Length > 100000)
            {
                ModelState.AddModelError(nameof(data.Photo), "Ukuran photo terlalu besar");
            }
            if (!ModelState.IsValid)
            {
                return View();
            }
            var filename = "photo_" + data.Username + Path.GetExtension(Photo.FileName);
            var filepath = Path.Combine(_env.WebRootPath,"upload", filename);

            using (var stream = System.IO.File.Create(filepath))
            {
                Photo.CopyTo(stream);
            }


            data.Photo =filename;

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

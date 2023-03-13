using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using MyBlog.Data;
using MyBlog.Models.ViewModel;
using System.Security.Claims;

namespace MyBlog.Controllers
{
    public class AccountController : Controller
    {
        private readonly MysqlContext _mysqlContext;

        public AccountController(MysqlContext mysqlContext)
        {
            _mysqlContext = mysqlContext;
        }
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login([FromForm] Login data)
        {
            var user = _mysqlContext.Users.Where(x => x.Username == data.Username && x.Password == data.Password).FirstOrDefault();
            if (user != null)
            {
                var claims = new List<Claim>()
                {
                    new Claim("username", user.Username),
                    new Claim("name", user.FullName),
                    new Claim("role","Admin")
                };

                var identity = new ClaimsIdentity(claims, "Cookie", "name", "role");
                var principal = new ClaimsPrincipal(identity);

                await HttpContext.SignInAsync(principal);

                return Redirect("/user/index");
            }

            return View();
        }
    }
}

using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Security.Claims;
using Workloopz.Data;
using Workloopz.Models;
using Workloopz.ViewModels;

namespace Workloopz.Controllers
{
    public class HomeController : Controller
    {
        private readonly NexTasksContext db;

        public HomeController(NexTasksContext context)

        {
            db = context;
        }
		[Authorize]
		public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(LoginVM model)
        {
            if (ModelState.IsValid) {
                var user = db.Users.SingleOrDefault(usr => usr.Username == model.Username);

                
                if (user != null)
                {
                    //Kiem tra mat khau neu ton tai User
                    if (user.Password == model.Password)
                    {
                        var claims = new List<Claim> {
                            new Claim (ClaimTypes.Name, user.FirstName + " " + user.LastName),
                            new Claim("UserID", user.Id.ToString()),
                        };
                        var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                        var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);
						await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, claimsPrincipal);
                        HttpContext.Session.SetString("fullname", user.FirstName + " " + user.LastName);
                        HttpContext.Session.SetInt32("userId", user.Id);
                        //Neu dung mat khau chuyen ve dashboard
                        return View("Index");
                    }
                    else {
                        //Neu sai mat khau  
                        ModelState.AddModelError("PasswordError", "Sai mật khẩu");
                    }

                }
                else
                {
                    //Neu user khong ton tai

                    ModelState.AddModelError("UsernameError", "Username không tồn tại");
                }
            }

            return View();
        }
		public IActionResult Register()
		{
			return View();
		}

	}
}

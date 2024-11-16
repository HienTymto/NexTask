using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
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
        [HttpGet("Dashboard")]
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
        public IActionResult Login(LoginVM model)
        {
            if (ModelState.IsValid) {
                var user = db.Users.SingleOrDefault(usr => usr.Username == model.Username);

                
                if (user != null)
                {
                    //Kiem tra mat khau neu ton tai User
                    if (user.Password == model.Password)
                    {
                        //Neu dung mat khau chuyen ve dashboard
                        return Redirect("/Dashboard");
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

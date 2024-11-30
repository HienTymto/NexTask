using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Workloopz.Data;
using Workloopz.Helpers;
using Workloopz.ViewModels;

namespace Workloopz.Controllers
{
    public class UserController : Controller
    {
        private readonly NexTasksContext db;
		private readonly IMapper _mapper;

		public UserController( NexTasksContext context, IMapper mapper)
        
        {
            db = context;
            _mapper = mapper;
        }
		public IActionResult Index()
		{
			var Users = db.Users.ToList();
			ViewData.Model = Users;
			return View("UserList");
		}
		[HttpGet]
        public IActionResult Register()
        {
            return View();
        }

		
		[HttpPost]
        public IActionResult Register(RegisterVM model)
        {
            if (ModelState.IsValid)
            {
                var user = _mapper.Map<User>(model);
                user.RandomKey = MyUtil.GenerateRandomKey();
                user.Password = model.Password.ToMd5Hash(user.RandomKey);
                user.Status = true;
                user.Bu = "IT";
				db.Add(user);
                db.SaveChanges();
                return RedirectToAction("Index");
			}
            
			return View();
		}
       

    }
}

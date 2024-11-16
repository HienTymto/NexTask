using Microsoft.AspNetCore.Mvc;
using Workloopz.Data;

namespace Workloopz.Controllers
{
    public class UserController : Controller
    {
        private readonly NexTasksContext db;

        public UserController( NexTasksContext context)
        
        {
            db = context;
        }
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

    }
}

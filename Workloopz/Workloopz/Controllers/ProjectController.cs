using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Workloopz.Data;
using Workloopz.Helpers;
using Workloopz.ViewModels;

namespace Workloopz.Controllers
{
	public class ProjectController : Controller
	{
		private readonly NexTasksContext db;
		private readonly IMapper _mapper;

		public ProjectController(NexTasksContext context, IMapper mapper)

		{
			db = context;
			_mapper = mapper;
		}
		[HttpGet]
		public IActionResult Index()
		{

			// Lấy danh sách dự án từ cơ sở dữ liệu
			try
			{
				var userId = Int32.Parse(User.FindFirst("UserID")?.Value); // Lấy UserID từ claims
				var CurUser = db.Users.Single(b => b.Id == userId);
				ViewBag.curUSer = CurUser; // Truyền vào View
				var projects = db.Projects.ToList();
				ViewBag.Projects = projects;
				return View("ListProject");
			}
			catch (Exception)
			{

				throw;
			}

		}
		public IActionResult CreateProject(ProjectVM model)
		{
			if (ModelState.IsValid)
			{
				var project = _mapper.Map<Project>(model);
				db.Add(project);
				db.SaveChanges();
				return RedirectToAction("Index");
			}

			return View();
		}
	}
}

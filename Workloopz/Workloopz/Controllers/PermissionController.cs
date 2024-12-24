using Microsoft.AspNetCore.Mvc;
using Workloopz.Data;

namespace Workloopz.Controllers
{
	public class PermissionController : Controller
	{
		private readonly NexTasksContext db;
		public PermissionController (NexTasksContext context)
		{
			db = context;
		}

		public IActionResult Index()
		{
			var permission = db.RolePermissions.Join(db.Roles, rp => rp.RoleId, r => r.Id,
				(rp, r) => new {rp , r}).
				Join(db.Permissions, rpr => rpr.rp.PermissionId, p => p.Id, 
				(rpr,p) => new {rpr , p}).Select(s => new
				{
					roleID = s.rpr.rp.RoleId,
					permissionID = s.rpr.rp.PermissionId,
					roleName = s.rpr.r.Name,
					permissionName = s.p.Name,
					codes = s.p.Code,
					typePermission = s.p.Type
				}).ToList();
			ViewBag.permission = permission;
			return View();
		}
	}
}

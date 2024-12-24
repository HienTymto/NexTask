using Workloopz.Data;

namespace Workloopz.Attribute
{
	public class CheckPermission
	{
		private readonly NexTasksContext db;
		public  CheckPermission (NexTasksContext context)
		{
			db = context;
		}
		public bool HasPermission(int userId, string permissionCode)
		{
			var user = db.Users.FirstOrDefault(u => u.Id == userId);
			if (user == null) return false;

			var hasPermission = db.RolePermissions
				.Join(db.Permissions,
					  rgp => rgp.PermissionId,
					  p => p.Id,
					  (rgp, p) => new { rgp.RoleId, p.Code })
				.Any(r => r.RoleId == user.RoleId && r.Code == permissionCode);

			return hasPermission;
		}
	}
}

using Azure.Core;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Workloopz.Data;
using Workloopz.ViewModels;

namespace Workloopz.Controllers
{
    [Route("api/permisson")]
    [ApiController]
    public class PermissionApiController : ControllerBase
    {
        private readonly NexTasksContext db;
        public PermissionApiController(NexTasksContext context)
        {
            db = context;
        }

		[HttpGet("data")]
		public IActionResult GetRolesWithPermissions()
		{
			var allPermissions = db.Permissions.ToList();
			var allRoles = db.Roles.ToList(); // Lấy tất cả các vai trò
			var allTypes = db.Permissions.Select(p => p.Type).Distinct().ToList();

			// Lấy dữ liệu Role với các Permission liên quan
			var rolesWithPermissions = allRoles.Select(role => new
			{
				RoleName = role.Name,
				Permissions = allTypes.Select(type => new
				{
					TypePermission = type,
					Permissions = allPermissions
						.Where(p => p.Type == type)
						.Select(p => new
						{
							PermissionId = p.Id,
							PermissionName = p.Name,
							Select = db.RolePermissions.Any(rp => rp.RoleId == role.Id && rp.PermissionId == p.Id)
						}).ToList()
				}).ToList(),
				AssignedPermissions = db.RolePermissions
					.Where(rp => rp.RoleId == role.Id)
					.Select(rp => rp.PermissionId)
					.ToList()
			}).ToList();

			return Ok(rolesWithPermissions);
		}

		[HttpPut("update")]
		public IActionResult UpdatePermission([FromBody] PermissionVM permission)
		{
			if (permission == null || string.IsNullOrEmpty(permission.RoleName) || permission.PermissionIds == null || !permission.PermissionIds.Any())
			{
				return BadRequest(new { message = "Thông tin không hợp lệ. Vui lòng kiểm tra lại dữ liệu." });
			}

			try
			{
				
				var role = db.Roles.FirstOrDefault(r => r.Name == permission.RoleName);
				if (role == null)
				{
					return NotFound(new { message = $"Vai trò '{permission.RoleName}' không tồn tại." });
				}

				
				var existingPermissions = db.RolePermissions.Where(rp => rp.RoleId == role.Id).ToList();

				
				var permissionsToRemove = existingPermissions
					.Where(rp => !permission.PermissionIds.Contains(rp.PermissionId))
					.ToList();
				db.RolePermissions.RemoveRange(permissionsToRemove);

				
				var existingPermissionIds = existingPermissions.Select(rp => rp.PermissionId).ToList();
				var permissionsToAdd = permission.PermissionIds
					.Where(id => !existingPermissionIds.Contains(id))
					.Select(id => new RolePermission
					{
						RoleId = role.Id,
						PermissionId = id
					}).ToList();
				db.RolePermissions.AddRange(permissionsToAdd);

				
				db.SaveChanges();

				
				return Ok(new { message = "Cập nhật quyền thành công!" });
			}
			catch (Exception ex)
			{
				return StatusCode(500, new { message = "Đã xảy ra lỗi trên server.", detail = ex.Message });
			}
		}





	}
}

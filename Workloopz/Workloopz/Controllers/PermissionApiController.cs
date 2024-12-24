using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Workloopz.Data;

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
        //[HttpGet("data")]
        //public IActionResult GetRolesWithPermissions()
        //{
        //    // Lấy toàn bộ dữ liệu thô trước
        //    var rawData = db.RolePermissions
        //        .Join(db.Roles, rp => rp.RoleId, r => r.Id, (rp, r) => new { rp.RoleId, RoleName = r.Name, rp.PermissionId })
        //        .Join(db.Permissions, rpr => rpr.PermissionId, p => p.Id, (rpr, p) => new { rpr.RoleName, p.Type, PermissionId = p.Id, PermissionName = p.Name })
        //        .ToList();

        //    // Xử lý GroupBy trong bộ nhớ
        //    var rolesWithPermissions = rawData
        //        .GroupBy(x => x.RoleName)
        //        .Select(group => new
        //        {
        //            RoleName = group.Key,
        //            Permissions = group
        //                .GroupBy(x => x.Type)
        //                .Select(typeGroup => new
        //                {
        //                    TypePermission = typeGroup.Key,
        //                    Permissions = typeGroup.Select(p => new
        //                    {
        //                        p.PermissionId,
        //                        p.PermissionName
        //                    }).ToList()
        //                }).ToList()
        //        }).ToList();

        //    return Ok(rolesWithPermissions);
        //}
        [HttpGet("data")]
        public IActionResult GetRolesWithPermissions()
        {
            // Lấy tất cả các quyền từ database (hoặc danh sách cố định nếu có)
            var allPermissions = db.Permissions.ToList();

            var rolesWithPermissions = db.RolePermissions
                .Join(db.Roles, rp => rp.RoleId, r => r.Id, (rp, r) => new { rp, r.Name })
                .Join(db.Permissions, rpr => rpr.rp.PermissionId, p => p.Id, (rpr, p) => new { rpr.Name, p })
                .ToList() // Chuyển về List để có thể nhóm (group) trên bộ nhớ
                .GroupBy(x => x.Name) // Group by Role Name
                .Select(group => new
                {
                    RoleName = group.Key,
                    Permissions = group
                        .GroupBy(x => x.p.Type) // Group by TypePermission
                        .Select(typeGroup => new
                        {
                            TypePermission = typeGroup.Key,
                            Permissions = allPermissions
                                .Where(p => p.Type == typeGroup.Key) // Lọc các quyền theo typePermission
                                .Select(p => new
                                {
                                    PermissionId = p.Id,
                                    PermissionName = p.Name,
                                    Select = typeGroup.Any(x => x.p.Id == p.Id) // Nếu quyền đã được cấp, set Select = true
                                }).ToList()
                        }).ToList(),
                    AssignedPermissions = db.RolePermissions
                        .Where(rp => rp.Role.Name == group.Key)
                        .Select(rp => rp.PermissionId).ToList() // Get assigned permissionIds for the role
                }).ToList();

            // Lọc thêm các TypePermission chưa được cấp quyền cho các roles (thêm vào nếu không có)
            var allTypes = db.Permissions.Select(p => p.Type).Distinct().ToList();
            foreach (var role in rolesWithPermissions)
            {
                foreach (var type in allTypes)
                {
                    if (!role.Permissions.Any(p => p.TypePermission == type))
                    {
                        role.Permissions.Add(new
                        {
                            TypePermission = type,
                            Permissions = allPermissions
                                .Where(p => p.Type == type)
                                .Select(p => new
                                {
                                    PermissionId = p.Id,
                                    PermissionName = p.Name,
                                    Select = false // Nếu chưa được cấp quyền, select = false
                                }).ToList()
                        });
                    }
                }
            }

            return Ok(rolesWithPermissions);
        }





    }
}

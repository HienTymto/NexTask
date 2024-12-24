using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using Workloopz.Data;

namespace Workloopz.Attribute
{
	public class AuthorizePermissionAttribute : ActionFilterAttribute
	{
		private readonly string _permissionCode;

		public AuthorizePermissionAttribute(string permissionCode)
		{
			_permissionCode = permissionCode;
		}

		public override void OnActionExecuting(ActionExecutingContext context)
		{
			var userId = context.HttpContext.Session.GetInt32("userId");

			
			if (userId == null)
			{
				context.Result = new RedirectToActionResult("Login", "Home", null); 
				return;
			}

			using (var db = new NexTasksContext())
			{
				var checker = new CheckPermission(db);

				
				if (!checker.HasPermission(userId.Value, _permissionCode))
				{
					
					(context.Controller as Controller)?.TempData.Add("ToastMessage", "Bạn không có quyền truy cập!");

					
					context.Result = new RedirectToActionResult("Index", "Home", null);
					return;
				}
			}

			
			base.OnActionExecuting(context);
		}
	}

}

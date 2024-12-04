using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Workloopz.Data;
using Workloopz.ViewModels;

namespace Workloopz.Controllers
{
	[Route("api/kanban")]
	[ApiController]
	public class KanbanController : ControllerBase
	{
		private readonly NexTasksContext db;
		private readonly IMapper _mapper;
		public KanbanController(NexTasksContext context, IMapper mapper)
		{
			db = context;
			_mapper = mapper;
		}
		//[HttpGet("gettasks/{statusId}")]
		//public IActionResult gettasks( int statusId) {

		//	var projectId = HttpContext.Session.GetInt32("projectId");
		//	if (projectId != null)
		//	{
		//		var tasks = db.Tasks
		//			.Where(t => t.ProjectId == projectId)
		//			.Where(t => t.StatusId == statusId)
		//			.Select(t => _mapper.Map<KanbanVM>(t))
		//			.ToList();
		//		return Ok(tasks);
		//	}
		//	var data = db.Tasks
		//		.Where(t => t.StatusId == statusId)
		//		.Select(t => _mapper.Map<KanbanVM>(t))
		//		.ToList();
		//	return Ok(data);
		//}
		[HttpGet("data")]
		
		public IActionResult getstatus() {
			var projectId = HttpContext.Session.GetInt32("projectId");
			if (projectId != null)
			{
				var datafproject = db.Statuses
				.Select(s => new
				{
					id = s.Id,
					title = s.Name,
					
					item = db.Tasks
					.Where (t => t.ProjectId == projectId)
					.Where(t => t.StatusId == s.Id)
					.Select(t => new
					{
                        id = t.Id,
                        title = t.Tittle,
					})
					.ToList()
				})
				.ToList();
				return Ok(datafproject);
			}
			var data = db.Statuses
				.Select(s => new
				{
                    id = s.Id,
                    title = s.Name,
                    item = db.Tasks
					.Where(t => t.StatusId == s.Id)
					.Select(t => new
                    {
                        id = t.Id,
                        title = t.Tittle,
                    })
					.ToList()
				})
				.ToList();
			return Ok(data);
		
		}
	}
}

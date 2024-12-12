using AutoMapper;
using Azure.Core;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Globalization;
using System.Threading.Tasks;
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
		[HttpGet("data")]
		public IActionResult getstatus()
		{
			var projectId = HttpContext.Session.GetInt32("projectId");
			if (projectId != null)
			{
				var datafproject = db.Statuses
				.Select(s => new
				{
					id = s.Id,
					title = s.Name,

					item = db.Tasks
					.Where(t => t.ProjectId == projectId)
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
		[HttpPost("createTask")]
		public IActionResult createTask([FromBody] KanbanVM kanbanVM)
		{

			try
			{
				var projectId = HttpContext.Session.GetInt32("projectId");
				var userId = HttpContext.Session.GetInt32("userId");
				if (userId == null)
				{
					return BadRequest("Không có giá trị User.");
				}
				var newTask = new Data.Task
				{					
					Tittle = kanbanVM.title ,
					StatusId = kanbanVM.status,  // statusId từ request
					Owner = userId.Value,
					ProjectId = projectId  
				};
				if (projectId != null)
				{
					newTask.ProjectId = projectId;
				}
				db.Tasks.Add(newTask);

				db.SaveChanges();
				return Ok(new { success = true, message = "Tạo công việc thành công", task = newTask });

			}
			catch (Exception)
			{

				throw;
			}

		}

		[HttpPut("updateTask")]
		public IActionResult UpdateTask([FromBody] KanbanVM kanban)
		{
			var dbTask = db.Tasks.Find(kanban.id);
			if (dbTask == null)
			{
				return NotFound(new { message = $"Task with ID {kanban.id} not found." });
			}

			dbTask.StatusId = kanban.status;

			try
			{
				db.SaveChanges();
				return Ok(new { action = "updated" });
			}
			catch (Exception)
			{
				return StatusCode(500, new { message = "Internal server error." });
			}
		}


	}
}

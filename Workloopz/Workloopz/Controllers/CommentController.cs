using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Workloopz.Data;

namespace Workloopz.Controllers
{
	[Route("api/comment")]
	[ApiController]
	public class CommentController : ControllerBase
	{
		private readonly NexTasksContext db;
		private readonly IMapper _mapper;
		public CommentController(NexTasksContext context, IMapper mapper)
		{
			db = context;
			_mapper = mapper;
		}
		[HttpGet("getcomments")]
		public async Task<IActionResult> GetComments(int taskId)
		{
			var comment =  db.Comments.Join(db.Users, c => c.UserId , u => u.Id, (c, u) => new {c, u})
				.Where(c => c.c.TaskId == taskId)
				.Select(t => new
				{
					id = t.c.Id,
					content = t.c.Contents,
					user = t.u.FirstName + t.u.LastName,
					createdAt = t.c.CreateAt
				}).ToList();
			return Ok(comment);
		}
	}
}

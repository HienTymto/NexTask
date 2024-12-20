using AutoMapper;
using Microsoft.AspNetCore.SignalR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using Workloopz.Data;
using Workloopz.ViewModels;

namespace Workloopz.Controllers
{
	[Route("api/comment")]
	[ApiController]
	public class CommentController : ControllerBase
	{
		private readonly IHubContext<ChatHub> _hubContext;
		private readonly NexTasksContext db;
		private readonly IMapper _mapper;
		public CommentController(NexTasksContext context, IMapper mapper,IHubContext<ChatHub> hubContext)
		{
			_hubContext = hubContext;
			db = context;
			_mapper = mapper;
		}
		//[HttpGet("getcomments")]
		//public async Task<IActionResult> GetComments(int taskId)
		//{
		//	var comment =  db.Comments.Join(db.Users, c => c.UserId , u => u.Id, (c, u) => new {c, u})
		//		.Where(c => c.c.TaskId == taskId)
		//		.Select(t => new
		//		{
		//			id = t.c.Id,
		//			content = t.c.Contents,
		//			user = t.u.FirstName +" "+ t.u.LastName,
		//			userid = t.c.UserId,
		//			createdAt = t.c.CreateAt
		//		}).ToList();
		//	return Ok(comment);
		//}
		[HttpGet("getcomments")]
		public async Task<IActionResult> GetComments(int taskId)
		{
			try
			{
				// Kiểm tra taskId có hợp lệ không
				if (taskId <= 0)
				{
					return BadRequest(new { success = false, message = "Invalid task ID" });
				}

				// Truy vấn lấy bình luận từ database
				var comment = db.Comments.Join(db.Users, c => c.UserId, u => u.Id, (c, u) => new { c, u })
					.Where(c => c.c.TaskId == taskId)
					.Select(t => new
					{
						id = t.c.Id,
						content = t.c.Contents,
						user = t.u.FirstName + " " + t.u.LastName,
						userid = t.c.UserId,
						createdAt = t.c.CreateAt
					}).ToList();

				// Trả về dữ liệu bình luận
				return Ok(comment);
			}
			catch (Exception ex)
			{
				// Log lỗi ra console (hoặc file log nếu có)
				Console.WriteLine("Error in GetComments: " + ex.Message);
				return StatusCode(500, new { success = false, message = ex.Message });
			}
		}

		[HttpGet("getname")]
		public IActionResult GetFullName()
		{
			var fullname = HttpContext.Session.GetString("fullname") ?? "Anonymous";
			return Ok(new { fullname });
		}
		[HttpPost("postcomment")]
		public async Task<IActionResult> PostComment([FromBody] commentVM comment)
		{
			try
			{
				var userId = HttpContext.Session.GetInt32("userId");
				if (userId == null)
				{
					return Unauthorized(new { success = false, message = "User is not logged in." });
				}

				var newComment = new Data.Comment
				{
					TaskId = comment.TaskId,
					Contents = comment.Contents,
					UserId = userId.Value,
					CreateAt = DateTime.UtcNow
				};

				db.Comments.Add(newComment);
				await db.SaveChangesAsync();

				// Phát sự kiện SignalR
				var userName = HttpContext.Session.GetString("fullname") ?? "Anonymous";
				await _hubContext.Clients.All.SendAsync("ReceiveMessage", userName, comment.Contents, comment.TaskId);
				Console.WriteLine($"SignalR Event: User {userName} posted '{comment.Contents}' on task {comment.TaskId}");

				return Ok(new { success = true, message = "Comment posted successfully!" });
			}
			catch (Exception ex)
			{
				return StatusCode(500, new { success = false, message = ex.Message });
			}
		}
	}
}

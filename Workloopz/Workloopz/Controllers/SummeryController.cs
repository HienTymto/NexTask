using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Workloopz.Data;

namespace Workloopz.Controllers
{
	[Route("api/summery")]
	[ApiController]
	public class SummeryController : ControllerBase
	{
		private readonly IMapper _mapper;
		private readonly NexTasksContext db;
		public SummeryController(NexTasksContext context, IMapper mapper)
		{

			_mapper = mapper;
			db = context;
		}
		[HttpGet("pieStatus")]
		public IActionResult GetpieStatus()
		{
			try
			{
				var projectID = HttpContext.Session.GetInt32("projectId");
				var dataStatus = db.Statuses
				.Select(s => new
				{
					statusId = s.Id,
					name = s.Name,
					counts = db.Tasks.Count(t => t.StatusId == s.Id),
				}).ToList();
				var labels = dataStatus.Select(d => d.name).ToList();
				var datasets = new List<object>
			{
				new
				{
					data = dataStatus.Select(d => d.counts).ToList(),
					backgroundColor = new List<string> { "#dee2e6", "#007bff", "#28a745" },
					borderColor = "transparent"
				}
			};

				var data = new
				{
					labels,
					datasets
				};
				return Ok(data);
			}
			catch (Exception)
			{
				throw;
			}
			
			
		}

	}
}

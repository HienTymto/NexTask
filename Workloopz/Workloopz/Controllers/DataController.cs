using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Workloopz.Data;
using Workloopz.ViewModels;
using Workloopz.Controllers;
using Microsoft.EntityFrameworkCore;
namespace Workloopz.Controllers
{
    [Produces("application/json")]
    [Route("api/data")]
	[ApiController]
	public class DataController : Controller
	{
		private readonly NexTasksContext db;
		private readonly IMapper _mapper;
		public DataController(NexTasksContext context, IMapper mapper) {
			db= context;
			_mapper= mapper;
		}
        [HttpGet]
        public object Get()
        {
            return new
            {
                data = db.Tasks.ToList().Select(t => _mapper.Map<GanttTaskVM>(t)),
                links = db.Links.ToList().Select(l => _mapper.Map<LinkVM>(l))

            };
        }
    }
	
}

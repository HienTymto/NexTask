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
    [Route("api/data/{id?}")]
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
             var projectID = RouteData.Values["id"];
            if (projectID != null && int.TryParse(projectID.ToString(), out var id))
            {
                return new
                {
                    data = db.Tasks.Where(t => t.ProjectId == id).Select(t => _mapper.Map<GanttTaskVM>(t)).ToList(),
                    links = db.Links.ToList().Select(l => _mapper.Map<LinkVM>(l))
                };
            }
            return new
            {
                data = db.Tasks.Select(t => _mapper.Map<GanttTaskVM>(t)).ToList(),
                links = db.Links.ToList().Select(l => _mapper.Map<LinkVM>(l))

            };

        }
    }
	
}

using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Workloopz.Data;
using Workloopz.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace Workloopz.Controllers
{
    [Produces("application/json")]
    [Route("api/task")]
    public class GanttTaskController : Controller
	{
        private readonly NexTasksContext _context;
        public GanttTaskController(NexTasksContext context)
        {
            _context = context;
        }

        // GET api/task
        [HttpGet]
        public IEnumerable<GanttTaskVM> Get()
        {
            return _context.Tasks
                .ToList()
                .Select(t => (GanttTaskVM)t);
        }

        // GET api/task/5
        [HttpGet("{id}")]
        public Data.Task? Get(int id)
        {
            return _context
                .Tasks
                .Find(id);
        }

        // POST api/task
        [HttpPost]
        public ObjectResult Post(GanttTaskVM apiTask)
        {
            try
            {
                var userId = Int32.Parse(User.FindFirst("UserID").Value);
                var newTask = (Data.Task)apiTask;
                newTask.Owner = userId;

                _context.Tasks.Add(newTask);

                _context.SaveChanges();
                return Ok(new
                {
                    tid = newTask.Id,
                    action = "inserted"
                });

            }
            catch (Exception)
            {

                throw ;
            }
            
            
        }

        //// PUT api/task/5
        //[HttpPut("{id}")]
        //public ObjectResult? Put(int id, WebApiTask apiTask)
        //{
        //    var updatedTask = (Models.Task)apiTask;
        //    var dbTask = _context.Tasks.Find(id);
        //    if (dbTask == null)
        //    {
        //        return null;
        //    }
        //    dbTask.Text = updatedTask.Text;
        //    dbTask.StartDate = updatedTask.StartDate;
        //    dbTask.Duration = updatedTask.Duration;
        //    dbTask.ParentId = updatedTask.ParentId;
        //    dbTask.Progress = updatedTask.Progress;
        //    dbTask.Type = updatedTask.Type;

        //    _context.SaveChanges();

        //    return Ok(new
        //    {
        //        action = "updated"
        //    });
        //}

        //// DELETE api/task/5
        //[HttpDelete("{id}")]
        //public ObjectResult DeleteTask(int id)
        //{
        //    var task = _context.Tasks.Find(id);
        //    if (task != null)
        //    {
        //        _context.Tasks.Remove(task);
        //        _context.SaveChanges();
        //    }

        //    return Ok(new
        //    {
        //        action = "deleted"
        //    });
        //}
    }

   
}

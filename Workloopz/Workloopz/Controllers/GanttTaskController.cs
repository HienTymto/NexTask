using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Workloopz.Data;
using Workloopz.ViewModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;

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
               .Select(t => (GanttTaskVM)t)
               .ToList();
            
         }

        //GET api/task/5
        [HttpGet("{id}")]
        public Data.Task? Get(int id)
        {
            return _context
                .Tasks
                .Find(id);
        }

        // POST api/task
        [HttpPost]
        public ObjectResult Post(GanttTaskVM ganttChartVM)
        {
            
            try
            {
                var projectId = HttpContext.Session.GetInt32("projectId");
                //System.Diagnostics.Debug.WriteLine(projectId == null ? "-1" : projectId);               
                var userId = HttpContext.Session.GetInt32("userId");
                if (userId == null)
                {
                    return BadRequest("Không có giá trị User.");
                }
                var newTask = (Data.Task)ganttChartVM;
                
                
                    newTask.Owner = userId.Value;
                
                if (projectId != null)
                {
                    newTask.ProjectId = projectId;
                }
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

                throw;
            }


        }

        // PUT api/task/5
        [HttpPut("{id}")]
        public ObjectResult? Put(int id, GanttTaskVM ganttTaskVM)
        {

            var updatedTask = (Data.Task)ganttTaskVM;
            var dbTask = _context.Tasks.Find(id);
            if (dbTask == null)
            {
                return null;
            }
            dbTask.Tittle = updatedTask.Tittle;
            dbTask.ScheduledTime = updatedTask.ScheduledTime;
            dbTask.Duration = updatedTask.Duration;
            dbTask.ParentId = updatedTask.ParentId;
            dbTask.Progress = updatedTask.Progress;
            dbTask.Type = updatedTask.Type;

            _context.SaveChanges();

            return Ok(new
            {
                action = "updated"
            });
        }

        // DELETE api/task/5
        [HttpDelete("{id}")]
        public ObjectResult DeleteTask(int id)
        {
            var task = _context.Tasks.Find(id);
            if (task != null)
            {
                _context.Tasks.Remove(task);
                _context.SaveChanges();
            }

            return Ok(new
            {
                action = "deleted"
            });
        }
    }


}

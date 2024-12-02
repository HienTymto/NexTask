using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Workloopz.Data;
using Workloopz.ViewModels;

namespace Workloopz.Controllers
{
    [Route("api/calendar")]
    [ApiController]
    public class CalendarController : ControllerBase
    {
        private readonly NexTasksContext db;
        private readonly IMapper _mapper;
        public CalendarController(NexTasksContext context, IMapper mapper)
        {
            db = context;
            _mapper = mapper;
        }
        [HttpGet("events")]
        public IActionResult GetEvents()
        {

            var projectId = HttpContext.Session.GetInt32("projectId");
            if (projectId != null)
            {
                var tasks = db.Tasks
                    .Where(t => t.ProjectId == projectId)
                    .Select(t => _mapper.Map<CalendarVM>(t))
                    .ToList();
                return Ok(tasks);
            }
            var events = db.Tasks
                .Select(t => _mapper.Map<CalendarVM>(t))
                .ToList();
            return Ok(events);
        }

        [HttpPost("create")]
        public IActionResult Create(CalendarVM calendar)
        {
            try
            {
                var projectId = HttpContext.Session.GetInt32("projectId");
                var userId = HttpContext.Session.GetInt32("userId");
                if (userId == null)
                {
                    return BadRequest("Không có giá trị User.");
                }
                var newTask = _mapper.Map<Data.Task>(calendar);
                newTask.Owner = userId.Value;
                if (projectId != null)
                {
                    newTask.ProjectId = projectId;
                }
                db.Tasks.Add(newTask);

                db.SaveChanges();
                return Ok(new { success = true, message = "Tạo công việc thành công", eventId = newTask.Id });

            }
            catch (Exception)
            {

                throw;
            }
        }



        [HttpPut("update/{id}")]
        public IActionResult Update(int id, CalendarVM calendar)
        {
            try
            {
                var dbTask = db.Tasks.FirstOrDefault(t => t.Id == id);
                if (dbTask == null)
                {
                    return NotFound(new { success = false, message = "Không tìm thấy công việc cần cập nhật?" });
                }

                dbTask.Tittle = calendar.title;


                dbTask.ScheduledTime = calendar.start;
                dbTask.ScheduledEnd = calendar.end;

                db.Tasks.Update(dbTask);
                db.SaveChanges();
                return Ok(new { success = true, message = "Cập nhật công việc thành công!" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { success = false, message = "Không thể cập nhật công việc :((", error = ex.Message });
            }
        }

        [HttpDelete("delete/{id}")]
        public IActionResult Delete(int id)
        {

            var tasks = db.Tasks.FirstOrDefault(t => t.Id == id);
            if (tasks != null)
            {
                db.Tasks.Remove(tasks);
                db.SaveChanges();
                return StatusCode(200);
            }
            else
            {
                return StatusCode(500);
            }
        }
    }
}

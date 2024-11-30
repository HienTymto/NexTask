using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Workloopz.Data;
using Workloopz.ViewModels;
namespace Workloopz.Controllers
{
    public class TaskController : Controller
    {
        private readonly NexTasksContext db;
        private readonly IMapper _mapper;

        public TaskController(NexTasksContext context, IMapper mapper)

        {
            db = context;
            _mapper = mapper;
        }
        public IActionResult TaskIndex()
        {
            return View("TaskIndex");
        }
        //Thêm công việc
        [HttpPost]
        public IActionResult CreateTask(TaskVM model)
        {
            if (ModelState.IsValid)
            {
                var task = _mapper.Map<Workloopz.Data.Task>(model);
                task.StatusId = 1;
                task.PriorityId = 2;
                db.Add(task);
                db.SaveChanges();
                return RedirectToAction("List", new { id = model.ProjectId });
            }

            return View();
        }
        //Xoá công việc
        [HttpPost]
        public IActionResult DeleteTask(int id, int ProjectId)
        {

            var task = db.Tasks.SingleOrDefault(t => t.Id == id);
            if (task == null)
            {
                return NotFound();
            }
            db.Tasks.Remove(task);
            db.SaveChanges();
            return RedirectToAction("List", new { id = ProjectId });
        }
        // Hiển thị tổng quan
        public IActionResult Summery()
        {
            var donetasks = db.Tasks.Count(t => t.StatusId == 3);
            var SevendayAgo = DateTime.Now.AddDays(-7);
            var createtasks = db.Tasks.Count(t => t.CreatedDate >= SevendayAgo);
            var UpdateTasks = db.Tasks.Count(t => t.Updated >= SevendayAgo);
            ViewBag.UpdateTasks = UpdateTasks;
            ViewBag.CreatedTask = createtasks;
            ViewBag.DoneTasks = donetasks;
            return View();
        }
        //Hiển thị dưới dạng bảng trạng thái
        public IActionResult Board()
        {
            return View();
        }
        //Hiển thị dưới dạng List
        public IActionResult List(int id)
        {// join table status
            var project = db.Projects.Where(p => p.Id == id).FirstOrDefault();
            var tasks = db.Tasks.Join(db.Statuses, t => t.StatusId, s => s.Id, (t, s) =>
            // join table proirrity
            new { t, s }).Join(db.Priorites, ts => ts.t.PriorityId, p => p.Id, (ts, p) =>
            // join table user
            new { ts, p }).Join(db.Users, tsu => tsu.ts.t.Owner, u => u.Id, (tsu, u) =>
            new { tsu, u }).Select(taskslect => new
            {
                Id = taskslect.tsu.ts.t.Id,
                Tittle = taskslect.tsu.ts.t.Tittle,
                Description = taskslect.tsu.ts.t.Description,
                ScheduledTime = taskslect.tsu.ts.t.ScheduledEnd,
                StatusName = taskslect.tsu.ts.s.Name,
                PriorityName = taskslect.tsu.p.Name,
                TaskOwner = taskslect.u.FirstName + " " + taskslect.u.LastName,
                ProjectId = taskslect.tsu.ts.t.ProjectId
            });
            TempData["ProjectName"] = project.Name;
            ViewBag.Tasks = tasks;
            return View();
        }
        //Hiển thị biểu đồ gantt
        public IActionResult Gantt()
        {
            return View();
        }
        


        public IActionResult Calendar()
        {
            return View();
        }
        public IActionResult Reports()
        {
            return View();
        }
        public IActionResult Attackment()
        {
            return View();
        }
    }
}

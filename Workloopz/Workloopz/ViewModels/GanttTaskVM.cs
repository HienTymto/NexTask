using Workloopz.Data;
namespace Workloopz.ViewModels
{
    public class GanttTaskVM
    {
        public int id { get; set; }
        public string? text { get; set; }
        public string? start_date { get; set; }
        public int? duration { get; set; }
        public decimal? progress { get; set; }
        public int? parent { get; set; }
        public string? type { get; set; }
        public int? projectId { get; set; }
        public bool open
        {
            get { return true; }
            set { }
        }

        public static explicit operator GanttTaskVM(Data.Task task)
        {
            return new GanttTaskVM
            {
                id = task.Id,
                text = task.Tittle,
                start_date = task.ScheduledTime.ToString("yyyy-MM-dd HH:mm"),
                duration = task.Duration,
                parent = task.ParentId,
                type = task.Type,
                progress = task.Progress,
                projectId = task.ProjectId,
            };
        }

        public static explicit operator Data.Task(GanttTaskVM task)
        {
            return new Data.Task
            {
                Id = task.id,
                Tittle = task.text,
                ScheduledTime = task.start_date != null ? DateTime.Parse(task.start_date,
                  System.Globalization.CultureInfo.InvariantCulture) : new DateTime(),
                Duration = task.duration,
                ParentId = task.parent,
                Type = task.type,
                Progress = task.progress,
                ProjectId = task.projectId,
            };
        }
    }
}

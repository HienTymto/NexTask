namespace Workloopz.ViewModels
{
    public class CalendarVM
    {
        public int id { get; set; }
        public string title { get; set; }
        public DateTime start { get; set; }
        public DateTime? end { get; set; }
        public string? description { get; set; }
        public string? color { get; set; }


        //public static explicit operator GanttTaskVM(Data.Task task)
        //{
        //    return new GanttTaskVM
        //    {
        //        id = task.Id,
        //        text = task.Tittle,
        //        start_date = task.ScheduledTime.Value.ToString("yyyy-MM-dd HH:mm"),
        //        duration = task.Duration,
        //        parent = task.ParentId,
        //        type = task.Type,
        //        progress = task.Progress,
        //        projectId = task.ProjectId,
        //    };
        //}

        //public static explicit operator Data.Task(GanttTaskVM task)
        //{
        //    return new Data.Task
        //    {
        //        Id = task.id,
        //        Tittle = task.text,
        //        ScheduledTime = task.start_date != null ? DateTime.Parse(task.start_date,
        //          System.Globalization.CultureInfo.InvariantCulture) : new DateTime(),
        //        Duration = task.duration,
        //        ParentId = task.parent,
        //        Type = task.type,
        //        Progress = task.progress,
        //        ProjectId = task.projectId,
        //    };
        //}
    }
}

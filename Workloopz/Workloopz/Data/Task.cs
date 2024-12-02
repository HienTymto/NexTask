using System;
using System.Collections.Generic;

namespace Workloopz.Data;

public partial class Task
{
    public int Id { get; set; }

    public string Tittle { get; set; } = null!;

    public string? Description { get; set; }

    public DateTime ScheduledTime { get; set; }

    public DateTime? ActualStart { get; set; }

    public DateTime? ActualEnd { get; set; }

    public DateTime? ScheduledEnd { get; set; }

    public int StatusId { get; set; }

    public int PriorityId { get; set; }

    public int Owner { get; set; }

    public int? ProjectId { get; set; }

    public DateTime? CreatedDate { get; set; }

    public DateTime? Updated { get; set; }

    public int? Duration { get; set; }

    public decimal? Progress { get; set; }

    public int? ParentId { get; set; }

    public string? Type { get; set; }

    public string? Color { get; set; }

    public virtual ICollection<Comment> Comments { get; set; } = new List<Comment>();

    public virtual ICollection<Link> LinkSourceTasks { get; set; } = new List<Link>();

    public virtual ICollection<Link> LinkTargetTasks { get; set; } = new List<Link>();

    public virtual User OwnerNavigation { get; set; } = null!;

    public virtual Priorite Priority { get; set; } = null!;

    public virtual Project? Project { get; set; }

    public virtual Status Status { get; set; } = null!;
}

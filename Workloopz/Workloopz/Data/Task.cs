using System;
using System.Collections.Generic;

namespace Workloopz.Data;

public partial class Task
{
    public int Id { get; set; }

    public string Tittle { get; set; } = null!;

    public string? Description { get; set; }

    public DateTime? ScheduledTime { get; set; }

    public DateTime? ActualStart { get; set; }

    public DateTime? ActualEnd { get; set; }

    public DateTime? ScheduledEnd { get; set; }

    public int StatusId { get; set; }

    public int PriorityId { get; set; }

    public int TypeId { get; set; }

    public int Creator { get; set; }

    public int Owner { get; set; }

    public virtual ICollection<Comment> Comments { get; set; } = new List<Comment>();

    public virtual User CreatorNavigation { get; set; } = null!;

    public virtual User OwnerNavigation { get; set; } = null!;

    public virtual Priorite Priority { get; set; } = null!;

    public virtual Status Status { get; set; } = null!;

    public virtual TaskType Type { get; set; } = null!;
}

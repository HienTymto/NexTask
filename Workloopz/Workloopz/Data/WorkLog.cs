using System;
using System.Collections.Generic;

namespace Workloopz.Data;

public partial class WorkLog
{
    public int Owner { get; set; }

    public DateTime? StartTime { get; set; }

    public DateTime? EndTime { get; set; }

    public int TaskId { get; set; }

    public string? Description { get; set; }

    public int? TypeId { get; set; }

    public long? Costs { get; set; }

    public virtual User OwnerNavigation { get; set; } = null!;

    public virtual Task Task { get; set; } = null!;

    public virtual WorkLogType? Type { get; set; }
}

using System;
using System.Collections.Generic;

namespace Workloopz.Data;

public partial class UserTask
{
    public int? UserId { get; set; }

    public int? TaskId { get; set; }

    public virtual Task? Task { get; set; }

    public virtual User? User { get; set; }
}

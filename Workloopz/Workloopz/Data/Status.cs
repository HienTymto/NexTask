using System;
using System.Collections.Generic;

namespace Workloopz.Data;

public partial class Status
{
    public string Name { get; set; } = null!;

    public int Id { get; set; }

    public virtual ICollection<Task> Tasks { get; set; } = new List<Task>();
}

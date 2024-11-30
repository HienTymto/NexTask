using System;
using System.Collections.Generic;

namespace Workloopz.Data;

public partial class Priorite
{
    public string Name { get; set; } = null!;

    public int Id { get; set; }

    public virtual ICollection<Task> Tasks { get; set; } = new List<Task>();
}

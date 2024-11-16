using System;
using System.Collections.Generic;

namespace Workloopz.Data;

public partial class Priorite
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<Task> Tasks { get; set; } = new List<Task>();
}

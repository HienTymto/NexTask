using System;
using System.Collections.Generic;

namespace Workloopz.Data;

public partial class Project
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public int Owner { get; set; }

    public string? Description { get; set; }

    public virtual User OwnerNavigation { get; set; } = null!;

    public virtual ICollection<Task> Tasks { get; set; } = new List<Task>();
}

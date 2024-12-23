﻿using System;
using System.Collections.Generic;

namespace Workloopz.Data;

public partial class TaskType
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<Task> Tasks { get; set; } = new List<Task>();
}

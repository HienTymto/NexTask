using System;
using System.Collections.Generic;

namespace Workloopz.Data;

public partial class Link
{
    public int Id { get; set; }

    public int SourceTaskId { get; set; }

    public int TargetTaskId { get; set; }

    public string Type { get; set; } = null!;

    public virtual Task SourceTask { get; set; } = null!;

    public virtual Task TargetTask { get; set; } = null!;
}

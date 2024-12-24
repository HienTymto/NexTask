using System;
using System.Collections.Generic;

namespace Workloopz.Data;

public partial class Permission
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string Code { get; set; } = null!;

    public string Type { get; set; } = null!;
}

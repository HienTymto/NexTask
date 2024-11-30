using System;
using System.Collections.Generic;

namespace Workloopz.Data;

public partial class UserProject
{
    public int? UserId { get; set; }

    public int? ProjectId { get; set; }

    public virtual Project? Project { get; set; }

    public virtual User? User { get; set; }
}
